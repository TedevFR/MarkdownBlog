using System.Text;

namespace MarkdownBlog.Generator;

public static class SitePageWriter
{
    public static void WriteHtml(
        GeneratorOptions options,
        string relativeOutputPath,
        string title,
        string description,
        string canonicalHref,
        string openGraphHtml,
        string bodyTemplateName,
        IDictionary<string, string> tokens)
    {
        var outputPath = Path.Combine(options.OutputDir, relativeOutputPath);
        Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);

        var baseHref = UrlBuilder.GetBaseHrefForOutputPath(relativeOutputPath);

        var bodyTemplate = TemplateRenderer.LoadTemplate(options.TemplatesDir, bodyTemplateName);
        var bodyHtml = TemplateRenderer.Render(bodyTemplate, tokens.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));

        var layoutTemplate = TemplateRenderer.LoadTemplate(options.TemplatesDir, "layout.html");
        var page = TemplateRenderer.Render(layoutTemplate, new Dictionary<string, string>
        {
            ["PAGE_TITLE"] = title,
            ["PAGE_DESCRIPTION"] = description,
            ["CANONICAL_URL"] = UrlBuilder.CanonicalHref(baseHref, canonicalHref),
            ["BASE"] = baseHref,
            ["OPEN_GRAPH"] = openGraphHtml ?? string.Empty,
            ["NAV_HOME_HREF"] = UrlBuilder.HrefFromRoot(baseHref, OutputPaths.HrefForHome()),
            ["NAV_POSTS_HREF"] = UrlBuilder.HrefFromRoot(baseHref, OutputPaths.HrefForPostsIndex()),
            ["NAV_ABOUT_HREF"] = UrlBuilder.HrefFromRoot(baseHref, OutputPaths.HrefForAbout()),
            ["BODY"] = bodyHtml,
            ["YEAR"] = DateTimeOffset.UtcNow.Year.ToString()
        });

        File.WriteAllText(outputPath, page, Encoding.UTF8);
    }
}
