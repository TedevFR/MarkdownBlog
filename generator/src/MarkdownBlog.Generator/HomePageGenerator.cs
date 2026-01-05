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
        var emptyState = $"<p>No posts yet.</p><p><a href=\"{aboutHref}\">Learn more on the About page</a></p>";

        var listHtml = PostListRenderer.Render(baseHref, latestPosts, includeExcerpt: true, emptyStateHtml: emptyState);

        SitePageWriter.WriteHtml(
            options,
            relativeOutputPath,
            title: "Home",
            description: "Latest posts",
            canonicalHref: OutputPaths.HrefForHome(),
            openGraphHtml: openGraph,
            bodyTemplateName: "home.html",
            tokens: new Dictionary<string, string>
            {
                ["HOME_TITLE"] = "Latest Posts",
                ["POST_LIST"] = listHtml,
            });
    }
}
