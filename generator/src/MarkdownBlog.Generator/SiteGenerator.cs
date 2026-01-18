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
                ["ABOUT_CONTENT"] = @"
                <div class=""about-content"">
                <p>
                    Je suis <b>Teddy Le Bras</b>, Technical Leader et développeur C# avec plus de 17 ans d’expérience dans la conception et le développement d’applications logicielles complexes.
                </p>
                
                <p>
                    Depuis le début de ma carrière, je navigue entre passion pour la technique, goût pour la résolution de problèmes et envie de transmettre.
                    Aujourd’hui, j’interviens chez Fnac, où j’accompagne une équipe de développeurs dans la conception de services critiques, la modernisation d’applications (.NET 4.8 vers .NET 10) et la migration vers le cloud Azure.
                    Mon quotidien, c’est autant du code que de l’architecture, du mentoring, des revues de code, et des échanges avec les Product Owners, QA et DevOps pour faire avancer les projets dans le bon sens.
                    J’ai notamment travaillé sur l'API de pricing, capables d’encaisser des pics à 1,2 million de requêtes par minute.
                </p>

                <p>
                    Ce qui me motive le plus, ce n’est pas seulement de faire fonctionner un système, mais de le rendre robuste, compréhensible et durable : clean architecture, qualité de code, automatisation, réduction de la dette technique, amélioration continue.
                    J’accorde aussi beaucoup d’importance à la transmission : accompagner les développeurs, faire grandir les équipes, partager les bonnes pratiques.
                </p>

                <p>
                    En parallèle de mon activité, je m’intéresse de très près à l’intelligence artificielle et à son impact sur notre métier.
                    Je fais partie d’une squad IA dédiée à la veille, à la formation et à la création d’outils pour améliorer la productivité des équipes.
                    <br />Ce blog est justement né de cette envie : explorer, comprendre, tester, prendre du recul… et partager.
                    <br />Tu trouveras ici des articles autour de :
                    <ul>
                        <li>la conception et le développement logiciel (.NET, architecture, bonnes pratiques)</li>
                        <li>le cloud et la performance</li>
                        <li>les outils et la productivité</li>
                        <li>l’intelligence artificielle et l’avenir du métier</li>
                        <li>et parfois, des réflexions plus personnelles sur la tech et le travail</li>
                    </ul>
                </p>
                </div>",
            });
    }   
}
