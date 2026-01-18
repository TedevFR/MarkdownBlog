namespace MarkdownBlog.Generator;

public sealed class Post
{
    public required string Title { get; init; }
    public string? TitleEn { get; init; }
    public required string Slug { get; init; }
    public required DateTimeOffset Date { get; init; }

    public string? Description { get; init; }
    public string? DescriptionEn { get; init; }
    public string? Excerpt { get; init; }
    public string? ExcerptEn { get; init; }

    public required string ContentMarkdownPath { get; init; }
    public required string ContentHtml { get; init; }
    public string? ContentHtmlEn { get; init; }

    public string? CoverImage { get; init; }
    public IReadOnlyList<string> Tags { get; init; } = Array.Empty<string>();

    public required string CanonicalHref { get; init; }
}
