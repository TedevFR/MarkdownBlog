namespace MarkdownBlog.Generator;

public static class PostFilter
{
    public static IReadOnlyList<Post> PublishableOnly(IEnumerable<Post> posts, DateTimeOffset now)
    {
        return posts
            .Where(p => p.Date <= now)
            .OrderByDescending(p => p.Date)
            .ToArray();
    }
}
