using System.Globalization;
using YamlDotNet.RepresentationModel;

namespace MarkdownBlog.Generator;

public static class FrontMatterParser
{
    public sealed record Result(IReadOnlyDictionary<string, object?> Data, string BodyMarkdown);

    public static Result Parse(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
        {
            return new Result(new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase), string.Empty);
        }

        var normalized = markdown.Replace("\r\n", "\n");
        if (!normalized.StartsWith("---\n", StringComparison.Ordinal))
        {
            return new Result(new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase), markdown);
        }

        var endIndex = normalized.IndexOf("\n---\n", "---\n".Length, StringComparison.Ordinal);
        if (endIndex < 0)
        {
            return new Result(new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase), markdown);
        }

        var yamlText = normalized.Substring("---\n".Length, endIndex - "---\n".Length);
        var bodyStart = endIndex + "\n---\n".Length;
        var body = normalized.Substring(bodyStart);

        var yaml = new YamlStream();
        using var reader = new StringReader(yamlText);
        yaml.Load(reader);

        var root = yaml.Documents.Count > 0 ? yaml.Documents[0].RootNode : null;
        var map = root as YamlMappingNode;

        var dict = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        if (map is null)
        {
            return new Result(dict, body);
        }

        foreach (var entry in map.Children)
        {
            var key = (entry.Key as YamlScalarNode)?.Value;
            if (string.IsNullOrWhiteSpace(key))
            {
                continue;
            }

            dict[key.Trim()] = ConvertYamlNode(entry.Value);
        }

        return new Result(dict, body);
    }

    public static string? GetString(IReadOnlyDictionary<string, object?> data, string key)
    {
        if (!data.TryGetValue(key, out var value) || value is null)
        {
            return null;
        }

        return value switch
        {
            string s => string.IsNullOrWhiteSpace(s) ? null : s.Trim(),
            _ => value.ToString()?.Trim(),
        };
    }

    public static IReadOnlyList<string> GetStringArray(IReadOnlyDictionary<string, object?> data, string key)
    {
        if (!data.TryGetValue(key, out var value) || value is null)
        {
            return Array.Empty<string>();
        }

        if (value is IReadOnlyList<object?> list)
        {
            return list
                .Select(v => v?.ToString())
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Select(v => v!.Trim())
                .ToArray();
        }

        var single = value.ToString();
        return string.IsNullOrWhiteSpace(single) ? Array.Empty<string>() : new[] { single.Trim() };
    }

    public static DateTimeOffset? GetDateTimeOffset(IReadOnlyDictionary<string, object?> data, string key)
    {
        var s = GetString(data, key);
        if (string.IsNullOrWhiteSpace(s))
        {
            return null;
        }

        if (DateTimeOffset.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var dto))
        {
            return dto;
        }

        if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var dt))
        {
            return new DateTimeOffset(dt);
        }

        return null;
    }

    public static (string? Fr, string? En) GetLocalizedString(IReadOnlyDictionary<string, object?> data, string key)
    {
        if (!data.TryGetValue(key, out var value) || value is null)
        {
            return (null, null);
        }

        // If it's a dictionary with fr/en keys
        if (value is IReadOnlyDictionary<string, object?> dict)
        {
            var fr = dict.TryGetValue("fr", out var frValue) ? frValue?.ToString()?.Trim() : null;
            var en = dict.TryGetValue("en", out var enValue) ? enValue?.ToString()?.Trim() : null;
            return (fr, en);
        }

        // If it's a simple string, use it for both languages (backward compatibility)
        var simple = value switch
        {
            string s => string.IsNullOrWhiteSpace(s) ? null : s.Trim(),
            _ => value.ToString()?.Trim(),
        };
        return (simple, simple);
    }

    private static object? ConvertYamlNode(YamlNode node)
    {
        return node switch
        {
            YamlScalarNode scalar => scalar.Value,
            YamlSequenceNode sequence => sequence.Children.Select(ConvertYamlNode).ToArray(),
            YamlMappingNode mapping => mapping.Children
                .Select(kvp => (
                    Key: (kvp.Key as YamlScalarNode)?.Value ?? string.Empty,
                    Value: ConvertYamlNode(kvp.Value)))
                .Where(x => !string.IsNullOrWhiteSpace(x.Key))
                .ToDictionary(x => x.Key, x => x.Value, StringComparer.OrdinalIgnoreCase),
            _ => null,
        };
    }
}
