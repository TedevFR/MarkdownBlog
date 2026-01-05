using System.Globalization;
using System.Text;

namespace MarkdownBlog.Generator;

public static class PostPageGenerator
{
    public static void Generate(GeneratorOptions options, Post post)
    {
        var relativeOutputPath = OutputPaths.PostDetail(post.Slug);
        var outputPath = Path.Combine(options.OutputDir, relativeOutputPath);
        Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);

        var baseHref = UrlBuilder.GetBaseHrefForOutputPath(relativeOutputPath);

        var bodyTemplate = TemplateRenderer.LoadTemplate(options.TemplatesDir, "post.html");
        var bodyHtml = TemplateRenderer.Render(bodyTemplate, new Dictionary<string, string>
        {
            ["POST_TITLE"] = post.Title,
            ["POST_DATE_ISO"] = post.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            ["POST_DATE_DISPLAY"] = post.Date.ToString("MMMM d, yyyy", CultureInfo.InvariantCulture),
            ["POST_CONTENT"] = post.ContentHtml,
        });

        var layoutTemplate = TemplateRenderer.LoadTemplate(options.TemplatesDir, "layout.html");
        var title = post.Title;
        var description = string.IsNullOrWhiteSpace(post.Description) ? post.Excerpt ?? string.Empty : post.Description;

        var openGraph = BuildOpenGraph(post, UrlBuilder.CanonicalHref(baseHref, post.CanonicalHref));

        var pageHtml = TemplateRenderer.Render(layoutTemplate, new Dictionary<string, string>
        {
            ["PAGE_TITLE"] = title,
            ["PAGE_DESCRIPTION"] = description,
            ["CANONICAL_URL"] = UrlBuilder.CanonicalHref(baseHref, post.CanonicalHref),
            ["BASE"] = baseHref,
            ["OPEN_GRAPH"] = openGraph,
            ["NAV_HOME_HREF"] = UrlBuilder.HrefFromRoot(baseHref, OutputPaths.HrefForHome()),
            ["NAV_POSTS_HREF"] = UrlBuilder.HrefFromRoot(baseHref, OutputPaths.HrefForPostsIndex()),
            ["NAV_ABOUT_HREF"] = UrlBuilder.HrefFromRoot(baseHref, OutputPaths.HrefForAbout()),
            ["BODY"] = bodyHtml,
            ["YEAR"] = DateTimeOffset.UtcNow.Year.ToString(CultureInfo.InvariantCulture),
            ["SITE_NAME"] = "Markdown Blog",
        });

        File.WriteAllText(outputPath, pageHtml, Encoding.UTF8);
    }

    private static string BuildOpenGraph(Post post, string canonicalHref)
    {
        var sb = new StringBuilder();
        sb.Append("<meta property=\"og:type\" content=\"article\" />\n");
        sb.Append($"<meta property=\"og:title\" content=\"{EscapeHtml(post.Title)}\" />\n");

        var description = string.IsNullOrWhiteSpace(post.Description) ? post.Excerpt ?? string.Empty : post.Description;
        if (!string.IsNullOrWhiteSpace(description))
        {
            sb.Append($"<meta property=\"og:description\" content=\"{EscapeHtml(description)}\" />\n");
        }

        if (!string.IsNullOrWhiteSpace(canonicalHref))
        {
            sb.Append($"<meta property=\"og:url\" content=\"{EscapeHtml(canonicalHref)}\" />\n");
        }

        if (!string.IsNullOrWhiteSpace(post.CoverImage))
        {
            sb.Append($"<meta property=\"og:image\" content=\"{EscapeHtml(post.CoverImage!)}\" />\n");
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
