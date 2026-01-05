namespace MarkdownBlog.Generator;

public sealed class Page
{
    public required string Key { get; init; }
    public required string Title { get; init; }

    public string? Description { get; init; }

    public required string BodyHtml { get; init; }
    public required string CanonicalHref { get; init; }
}
