using System.Text;

namespace MarkdownBlog.Generator;

public static class SiteGenerator
{
    public static void Generate(GeneratorOptions options)
    {
        ValidateDirs(options);
        Directory.CreateDirectory(options.OutputDir);

        AssetPipeline.CopyAssets(options.AssetsDir, options.PublicDir, options.OutputDir);

        var allPosts = PostLoader.LoadPosts(options.InputDir);
        var posts = PostFilter.PublishableOnly(allPosts, DateTimeOffset.UtcNow);

        GenerateNotFound(options);
        GenerateAbout(options);

        foreach (var post in posts)
        {
            PostPageGenerator.Generate(options, post);
        }

        HomePageGenerator.Generate(options, posts.Take(10).ToArray());
        PostsIndexGenerator.Generate(options, posts);

        SitemapGenerator.WriteRobotsAndSitemap(Path.Combine(options.OutputDir, "public"),
            new[]
            {
                OutputPaths.HrefForHome(),
                OutputPaths.HrefForPostsIndex(),
                OutputPaths.HrefForAbout(),
            }.Concat(posts.Select(p => p.CanonicalHref)));

        LinkChecker.ThrowIfBroken(options.OutputDir);
    }

    private static void ValidateDirs(GeneratorOptions options)
    {
        if (!Directory.Exists(options.InputDir))
        {
            throw new DirectoryNotFoundException($"Input directory not found: {options.InputDir}");
        }

        if (!Directory.Exists(options.TemplatesDir))
        {
            throw new DirectoryNotFoundException($"Templates directory not found: {options.TemplatesDir}");
        }

        if (!Directory.Exists(options.AssetsDir))
        {
            throw new DirectoryNotFoundException($"Assets directory not found: {options.AssetsDir}");
        }

        if (!Directory.Exists(options.PublicDir))
        {
            Directory.CreateDirectory(options.PublicDir);
        }
    }

    private static void GenerateNotFound(GeneratorOptions options)
    {
        SitePageWriter.WriteHtml(
            options,
            OutputPaths.NotFound,
            title: "Page not found",
            description: "The page you requested could not be found.",
            canonicalHref: "404.html",
            openGraphHtml: string.Empty,
            bodyTemplateName: "404.html",
            tokens: new Dictionary<string, string>
            {
                ["HOME_HREF"] = UrlBuilder.HrefFromRoot(UrlBuilder.GetBaseHrefForOutputPath(OutputPaths.NotFound), OutputPaths.HrefForHome()),
            });
    }

    private static void GenerateAbout(GeneratorOptions options)
    {
        var baseHref = UrlBuilder.GetBaseHrefForOutputPath(OutputPaths.About);
        var canonical = UrlBuilder.CanonicalHref(baseHref, OutputPaths.HrefForAbout());
        var openGraph = OpenGraph.Website("About", "About this blog", canonical);

        SitePageWriter.WriteHtml(
            options,
            OutputPaths.About,
            title: "About",
            description: "About this blog",
            canonicalHref: OutputPaths.HrefForAbout(),
            openGraphHtml: openGraph,
            bodyTemplateName: "about.html",
            tokens: new Dictionary<string, string>
            {
                ["ABOUT_TITLE"] = "About",
                ["ABOUT_CONTENT"] = "<p>A lightweight blog generated from Markdown.</p>",
            });
    }   
}
