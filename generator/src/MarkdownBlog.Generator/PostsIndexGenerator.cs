namespace MarkdownBlog.Generator;

public static class PostsIndexGenerator
{
    public static void Generate(GeneratorOptions options, IReadOnlyList<Post> posts)
    {
        var relativeOutputPath = OutputPaths.PostsIndex;
        var baseHref = UrlBuilder.GetBaseHrefForOutputPath(relativeOutputPath);

        var canonical = UrlBuilder.CanonicalHref(baseHref, OutputPaths.HrefForPostsIndex());
        var openGraph = OpenGraph.Website("All Posts", "Browse all posts", canonical);

        var aboutHref = UrlBuilder.HrefFromRoot(baseHref, OutputPaths.HrefForAbout());
        var emptyState = $"<p data-i18n=\"list.empty\">Pas encore d'articles.</p><p><a href=\"{aboutHref}\" data-i18n=\"list.empty.link\">En savoir plus sur la page À propos</a></p>";

        var listHtml = PostListRenderer.Render(baseHref, posts, includeExcerpt: false, emptyStateHtml: emptyState);

        SitePageWriter.WriteHtml(
            options,
            relativeOutputPath,
            title: "Tous les articles",
            description: "Browse all posts",
            canonicalHref: OutputPaths.HrefForPostsIndex(),
            openGraphHtml: openGraph,
            bodyTemplateName: "list.html",
            tokens: new Dictionary<string, string>
            {
                ["POST_LIST"] = listHtml,
            });
    }
}
