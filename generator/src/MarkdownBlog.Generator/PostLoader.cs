namespace MarkdownBlog.Generator;

public static class PostLoader
{
    public static IReadOnlyList<Post> LoadPosts(string inputDir)
    {
        var postsDir = Path.Combine(inputDir, "posts");
        if (!Directory.Exists(postsDir))
        {
            return Array.Empty<Post>();
        }

        var posts = new List<Post>();
        foreach (var filePath in Directory.EnumerateFiles(postsDir, "*.md", SearchOption.TopDirectoryOnly))
        {
            var markdown = File.ReadAllText(filePath);
            var parsed = FrontMatterParser.Parse(markdown);

            var title = FrontMatterParser.GetString(parsed.Data, "title");
            var date = FrontMatterParser.GetDateTimeOffset(parsed.Data, "date");
            if (string.IsNullOrWhiteSpace(title) || !date.HasValue)
            {
                continue;
            }

            var slug = FrontMatterParser.GetString(parsed.Data, "slug");
            slug = string.IsNullOrWhiteSpace(slug) ? Slugifier.Slugify(title) : Slugifier.Slugify(slug);

            var excerpt = ExcerptGenerator.Generate(FrontMatterParser.GetString(parsed.Data, "excerpt"), parsed.BodyMarkdown);
            var cover = FrontMatterParser.GetString(parsed.Data, "cover");
            var tags = FrontMatterParser.GetStringArray(parsed.Data, "tags");

            var html = MarkdownConverter.ToHtml(parsed.BodyMarkdown);

            posts.Add(new Post
            {
                Title = title,
                Slug = slug,
                Date = date.Value,
                Excerpt = excerpt,
                Description = excerpt,
                ContentMarkdownPath = filePath,
                ContentHtml = html,
                CoverImage = cover,
                Tags = tags,
                CanonicalHref = OutputPaths.HrefForPostDetail(slug),
            });
        }

        // Stable ordering is handled by PostFilter.
        return posts;
    }
}
