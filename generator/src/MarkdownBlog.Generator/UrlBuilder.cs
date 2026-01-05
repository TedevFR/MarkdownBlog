namespace MarkdownBlog.Generator;

public static class UrlBuilder
{
    public static string GetBaseHrefForOutputPath(string relativeOutputPath)
    {
        var dir = Path.GetDirectoryName(relativeOutputPath)?.Replace('\\', '/');
        if (string.IsNullOrEmpty(dir) || dir == ".")
        {
            return string.Empty;
        }

        var segments = dir.Split('/', StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(Enumerable.Repeat("../", segments.Length));
    }

    public static string HrefFromRoot(string baseHref, string rootRelativeHref)
    {
        var cleaned = rootRelativeHref.TrimStart('/');
        if (string.IsNullOrEmpty(cleaned))
        {
            return string.IsNullOrEmpty(baseHref) ? "./" : baseHref;
        }

        return baseHref + cleaned;
    }

    public static string CanonicalHref(string baseHref, string rootRelativeHref)
    {
        return HrefFromRoot(baseHref, rootRelativeHref);
    }
}
