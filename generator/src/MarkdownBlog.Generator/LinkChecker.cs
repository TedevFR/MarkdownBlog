using System.Text.RegularExpressions;

namespace MarkdownBlog.Generator;

public static partial class LinkChecker
{
    public sealed record BrokenLink(string SourceFile, string Link);

    public static IReadOnlyList<BrokenLink> ValidateDist(string distDir)
    {
        if (!Directory.Exists(distDir))
        {
            return [];
        }

        var broken = new List<BrokenLink>();
        foreach (var htmlPath in Directory.EnumerateFiles(distDir, "*.html", SearchOption.AllDirectories))
        {
            var html = File.ReadAllText(htmlPath);
            var baseDir = Path.GetDirectoryName(htmlPath)!;

            foreach (Match match in HrefOrSrcRegex().Matches(html))
            {
                var raw = match.Groups[1].Value;
                if (string.IsNullOrWhiteSpace(raw))
                {
                    continue;
                }

                var link = StripQueryAndHash(raw.Trim());
                if (ShouldIgnore(link))
                {
                    continue;
                }

                var targetPath = ResolveTargetPath(distDir, baseDir, link);
                if (targetPath is null)
                {
                    continue;
                }

                if (!File.Exists(targetPath) && !Directory.Exists(targetPath))
                {
                    broken.Add(new BrokenLink(htmlPath, raw));
                }
            }
        }

        return broken;
    }

    public static void ThrowIfBroken(string distDir)
    {
        var broken = ValidateDist(distDir);
        if (broken.Count == 0)
        {
            return;
        }

        var message = string.Join(Environment.NewLine, broken.Select(b => $"Broken link in {b.SourceFile}: {b.Link}"));
        throw new InvalidOperationException(message);
    }

    private static string? ResolveTargetPath(string distDir, string currentHtmlDir, string link)
    {
        var normalized = link.Replace('\\', '/');

        // Treat leading slash as dist-root relative.
        if (normalized.StartsWith("/", StringComparison.Ordinal))
        {
            normalized = normalized.TrimStart('/');
            return MapToDist(distDir, normalized);
        }

        // Relative to current HTML file.
        var combined = Path.GetFullPath(Path.Combine(currentHtmlDir, normalized));

        // Prevent escaping dist.
        var distFull = Path.GetFullPath(distDir);
        if (!combined.StartsWith(distFull, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (Directory.Exists(combined))
        {
            return Path.Combine(combined, "index.html");
        }

        if (combined.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
        {
            return Path.Combine(combined, "index.html");
        }

        if (combined.EndsWith("/", StringComparison.Ordinal))
        {
            return Path.Combine(combined, "index.html");
        }

        // Map trailing slash semantics
        if (normalized.EndsWith("/", StringComparison.Ordinal))
        {
            combined = Path.Combine(currentHtmlDir, normalized, "index.html");
        }
        else if (!Path.HasExtension(combined))
        {
            // Route-like href (e.g. "posts/") -> index.html
            combined = Path.Combine(combined, "index.html");
        }

        return combined;
    }

    private static string MapToDist(string distDir, string rootRel)
    {
        var p = Path.Combine(distDir, rootRel.Replace('/', Path.DirectorySeparatorChar));
        if (!Path.HasExtension(p))
        {
            p = Path.Combine(p, "index.html");
        }
        return p;
    }

    private static bool ShouldIgnore(string link)
    {
        if (string.IsNullOrWhiteSpace(link))
        {
            return true;
        }

        if (link.StartsWith("#", StringComparison.Ordinal))
        {
            return true;
        }

        return link.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
            || link.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
            || link.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase)
            || link.StartsWith("tel:", StringComparison.OrdinalIgnoreCase)
            || link.StartsWith("data:", StringComparison.OrdinalIgnoreCase);
    }

    private static string StripQueryAndHash(string link)
    {
        var q = link.IndexOf('?');
        if (q >= 0)
        {
            link = link[..q];
        }

        var h = link.IndexOf('#');
        if (h >= 0)
        {
            link = link[..h];
        }

        return link;
    }

    [GeneratedRegex("(?:href|src)=\\\"([^\\\"]+)\\\"", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
    private static partial Regex HrefOrSrcRegex();
}
