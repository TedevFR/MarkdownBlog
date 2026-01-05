using System.Text;

namespace MarkdownBlog.Generator;

public static class OpenGraph
{
    public static string Website(string title, string description, string canonicalHref, string? imageHref = null)
    {
        var sb = new StringBuilder();
        sb.Append("<meta property=\"og:type\" content=\"website\" />\n");
        sb.Append($"<meta property=\"og:title\" content=\"{EscapeHtml(title)}\" />\n");

        if (!string.IsNullOrWhiteSpace(description))
        {
            sb.Append($"<meta property=\"og:description\" content=\"{EscapeHtml(description)}\" />\n");
        }

        if (!string.IsNullOrWhiteSpace(canonicalHref))
        {
            sb.Append($"<meta property=\"og:url\" content=\"{EscapeHtml(canonicalHref)}\" />\n");
        }

        if (!string.IsNullOrWhiteSpace(imageHref))
        {
            sb.Append($"<meta property=\"og:image\" content=\"{EscapeHtml(imageHref)}\" />\n");
        }

        return sb.ToString();
    }

    private static string EscapeHtml(string s)
    {
        return s
            .Replace("&", "&amp;", StringComparison.Ordinal)
            .Replace("<", "&lt;", StringComparison.Ordinal)
            .Replace(">", "&gt;", StringComparison.Ordinal)
            .Replace("\"", "&quot;", StringComparison.Ordinal)
            .Replace("'", "&#39;", StringComparison.Ordinal);
    }
}
