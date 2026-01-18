namespace MarkdownBlog.Generator;

public static class PostLoader
{
    private const string LangSeparator = "<!-- lang:en -->";

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

            var (titleFr, titleEn) = FrontMatterParser.GetLocalizedString(parsed.Data, "title");
            var date = FrontMatterParser.GetDateTimeOffset(parsed.Data, "date");
            if (string.IsNullOrWhiteSpace(titleFr) || !date.HasValue)
            {
                continue;
            }

            var slug = FrontMatterParser.GetString(parsed.Data, "slug");
            slug = string.IsNullOrWhiteSpace(slug) ? Slugifier.Slugify(titleFr) : Slugifier.Slugify(slug);

            var (excerptFr, excerptEn) = FrontMatterParser.GetLocalizedString(parsed.Data, "excerpt");
            var cover = FrontMatterParser.GetString(parsed.Data, "cover");
            var tags = FrontMatterParser.GetStringArray(parsed.Data, "tags");

            // Split content by language separator
            var (contentFr, contentEn) = SplitContentByLanguage(parsed.BodyMarkdown);

            var htmlFr = MarkdownConverter.ToHtml(contentFr);
            var htmlEn = !string.IsNullOrWhiteSpace(contentEn) ? MarkdownConverter.ToHtml(contentEn) : null;

            // Generate excerpt from content if not provided in front matter
            var finalExcerptFr = ExcerptGenerator.Generate(excerptFr, contentFr);
            var finalExcerptEn = !string.IsNullOrWhiteSpace(excerptEn) 
                ? excerptEn 
                : (!string.IsNullOrWhiteSpace(contentEn) ? ExcerptGenerator.Generate(null, contentEn) : null);

            posts.Add(new Post
            {
                Title = titleFr,
                TitleEn = titleEn,
                Slug = slug,
                Date = date.Value,
                Excerpt = finalExcerptFr,
                ExcerptEn = finalExcerptEn,
                Description = finalExcerptFr,
                DescriptionEn = finalExcerptEn,
                ContentMarkdownPath = filePath,
                ContentHtml = htmlFr,
                ContentHtmlEn = htmlEn,
                CoverImage = cover,
                Tags = tags,
                CanonicalHref = OutputPaths.HrefForPostDetail(slug),
            });
        }

        // Stable ordering is handled by PostFilter.
        return posts;
    }

    private static (string Fr, string? En) SplitContentByLanguage(string bodyMarkdown)
    {
        var separatorIndex = bodyMarkdown.IndexOf(LangSeparator, StringComparison.OrdinalIgnoreCase);
        
        if (separatorIndex < 0)
        {
            return (bodyMarkdown.Trim(), null);
        }

        var frContent = bodyMarkdown[..separatorIndex].Trim();
        var enContent = bodyMarkdown[(separatorIndex + LangSeparator.Length)..].Trim();

        return (frContent, string.IsNullOrWhiteSpace(enContent) ? null : enContent);
    }
}
