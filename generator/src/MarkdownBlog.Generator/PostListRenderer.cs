using System.Globalization;
using System.Text;

namespace MarkdownBlog.Generator;

public static class PostListRenderer
{
    public static string Render(string baseHref, IReadOnlyList<Post> posts, bool includeExcerpt, string emptyStateHtml)
    {
        if (posts.Count == 0)
        {
            return emptyStateHtml;
        }

        var sb = new StringBuilder();
        sb.Append("<ul>\n");

        foreach (var post in posts)
        {
            var href = UrlBuilder.HrefFromRoot(baseHref, post.CanonicalHref);
            var dateIso = post.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dateDisplay = post.Date.ToString("MMMM d, yyyy", CultureInfo.InvariantCulture);

            sb.Append("  <li>\n");
            sb.Append($"    <a href=\"{href}\">{EscapeHtml(post.Title)}</a>\n");
            sb.Append($"    <div><time datetime=\"{dateIso}\">{EscapeHtml(dateDisplay)}</time></div>\n");

            if (includeExcerpt && !string.IsNullOrWhiteSpace(post.Excerpt))
            {
                sb.Append($"    <p>{EscapeHtml(post.Excerpt!)}</p>\n");
            }

            sb.Append("  </li>\n");
        }

        sb.Append("</ul>\n");
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
