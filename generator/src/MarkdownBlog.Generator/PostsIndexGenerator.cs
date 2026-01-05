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
        var emptyState = $"<p>No posts yet.</p><p><a href=\"{aboutHref}\">Learn more on the About page</a></p>";

        var listHtml = PostListRenderer.Render(baseHref, posts, includeExcerpt: false, emptyStateHtml: emptyState);

        SitePageWriter.WriteHtml(
            options,
            relativeOutputPath,
            title: "All Posts",
            description: "Browse all posts",
            canonicalHref: OutputPaths.HrefForPostsIndex(),
            openGraphHtml: openGraph,
            bodyTemplateName: "list.html",
            tokens: new Dictionary<string, string>
            {
                ["LIST_TITLE"] = "All Posts",
                ["POST_LIST"] = listHtml,
            });
    }
}
