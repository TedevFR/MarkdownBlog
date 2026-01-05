namespace MarkdownBlog.Generator;

public static class OutputPaths
{
    public static string Home => "index.html";
    public static string PostsIndex => Path.Combine("posts", "index.html");
    public static string About => Path.Combine("about", "index.html");
    public static string NotFound => "404.html";

    public static string PostDetail(string slug) => Path.Combine("posts", slug, "index.html");

    public static string HrefForHome() => "index.html";
    public static string HrefForPostsIndex() => "posts/index.html";
    public static string HrefForAbout() => "about/index.html";
    public static string HrefForPostDetail(string slug) => $"posts/{slug}/index.html";
}
