namespace MarkdownBlog.Generator;

public static class HomePageGenerator
{
    public static void Generate(GeneratorOptions options, IReadOnlyList<Post> latestPosts)
    {
        var relativeOutputPath = OutputPaths.Home;
        var baseHref = UrlBuilder.GetBaseHrefForOutputPath(relativeOutputPath);

        var canonical = UrlBuilder.CanonicalHref(baseHref, OutputPaths.HrefForHome());
        var openGraph = OpenGraph.Website("Home", "Latest posts", canonical);

        var aboutHref = UrlBuilder.HrefFromRoot(baseHref, OutputPaths.HrefForAbout());
        var emptyState = $"<p data-i18n=\"home.empty\">Pas encore d'articles.</p><p><a href=\"{aboutHref}\" data-i18n=\"home.empty.link\">En savoir plus sur la page À propos</a></p>";

        var listHtml = PostListRenderer.Render(baseHref, latestPosts, includeExcerpt: true, emptyStateHtml: emptyState);

        SitePageWriter.WriteHtml(
            options,
            relativeOutputPath,
            title: "Accueil",
            description: "Derniers posts",
            canonicalHref: OutputPaths.HrefForHome(),
            openGraphHtml: openGraph,
            bodyTemplateName: "home.html",
            tokens: new Dictionary<string, string>
            {
                ["POST_LIST"] = listHtml,
            });
    }
}
