using System.Text;
using Xunit;

namespace MarkdownBlog.Generator.Tests.Integration;

public sealed class PostPageTests
{
    [Fact]
    public void GeneratePostPage_WritesExpectedHtml()
    {
        var root = Path.Combine(Path.GetTempPath(), "MarkdownBlogTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(root);

        try
        {
            var templatesDir = Path.Combine(root, "templates");
            var assetsDir = Path.Combine(root, "assets");
            var publicDir = Path.Combine(root, "public");
            var outputDir = Path.Combine(root, "dist");

            Directory.CreateDirectory(templatesDir);
            Directory.CreateDirectory(assetsDir);
            Directory.CreateDirectory(publicDir);

            File.WriteAllText(Path.Combine(templatesDir, "layout.html"), "<html><head><title>{{PAGE_TITLE}}</title></head><body>{{BODY}}</body></html>", Encoding.UTF8);
            File.WriteAllText(Path.Combine(templatesDir, "post.html"), "<h1>{{POST_TITLE}}</h1><time datetime=\"{{POST_DATE_ISO}}\">{{POST_DATE_DISPLAY}}</time>{{POST_CONTENT}}", Encoding.UTF8);

            var options = new MarkdownBlog.Generator.GeneratorOptions(
                InputDir: root,
                TemplatesDir: templatesDir,
                AssetsDir: assetsDir,
                PublicDir: publicDir,
                OutputDir: outputDir);

            var post = new MarkdownBlog.Generator.Post
            {
                Title = "First Post",
                Slug = "first-post",
                Date = new DateTimeOffset(2025, 12, 18, 0, 0, 0, TimeSpan.Zero),
                Excerpt = "Hello there",
                ContentMarkdownPath = "content/posts/first-post.md",
                ContentHtml = "<p>Body</p>",
                CanonicalHref = MarkdownBlog.Generator.OutputPaths.HrefForPostDetail("first-post"),
            };

            MarkdownBlog.Generator.PostPageGenerator.Generate(options, post);

            var outputPath = Path.Combine(outputDir, MarkdownBlog.Generator.OutputPaths.PostDetail("first-post"));
            Assert.True(File.Exists(outputPath));

            var html = File.ReadAllText(outputPath, Encoding.UTF8);
            Assert.Contains("<h1>First Post</h1>", html);
            Assert.Contains("<p>Body</p>", html);
        }
        finally
        {
            try { Directory.Delete(root, recursive: true); } catch { /* best-effort */ }
        }
    }
}
