using System.Text;

namespace MarkdownBlog.Generator;

public static class TemplateRenderer
{
    public static string LoadTemplate(string templatesDir, string fileName)
    {
        var path = Path.Combine(templatesDir, fileName);
        return File.ReadAllText(path, Encoding.UTF8);
    }

    public static string Render(string template, IReadOnlyDictionary<string, string> tokens)
    {
        var result = template;
        foreach (var (key, value) in tokens)
        {
            result = result.Replace("{{" + key + "}}", value ?? string.Empty, StringComparison.Ordinal);
        }

        return result;
    }
}
