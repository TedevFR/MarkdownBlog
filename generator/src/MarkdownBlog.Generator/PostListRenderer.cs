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

            var titleFr = EscapeHtml(post.Title);
            var titleEn = EscapeHtml(post.TitleEn ?? post.Title);

            sb.Append("  <li>\n");
            sb.Append($"    <a href=\"{href}\">");
            sb.Append($"<span data-i18n-content=\"title-fr\">{titleFr}</span>");
            sb.Append($"<span data-i18n-content=\"title-en\" style=\"display:none;\">{titleEn}</span>");
            sb.Append("</a>\n");
            sb.Append($"    <div><time datetime=\"{dateIso}\" data-i18n-date=\"{dateIso}\"></time></div>\n");

            if (includeExcerpt)
            {
                var excerptFr = post.Excerpt;
                var excerptEn = post.ExcerptEn ?? post.Excerpt;

                if (!string.IsNullOrWhiteSpace(excerptFr))
                {
                    sb.Append($"    <p data-i18n-content=\"excerpt-fr\">{EscapeHtml(excerptFr)}</p>\n");
                }
                if (!string.IsNullOrWhiteSpace(excerptEn))
                {
                    sb.Append($"    <p data-i18n-content=\"excerpt-en\" style=\"display:none;\">{EscapeHtml(excerptEn)}</p>\n");
                }
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
