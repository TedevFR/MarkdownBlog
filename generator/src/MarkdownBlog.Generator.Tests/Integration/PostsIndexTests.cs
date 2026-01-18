using System.Text;
using Xunit;

namespace MarkdownBlog.Generator.Tests.Integration;

public sealed class PostsIndexTests
{
    [Fact]
    public void GeneratePostsIndex_ListsAllPostsOnce()
    {
        var root = Path.Combine(Path.GetTempPath(), "MarkdownBlogTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(root);

        try
        {
            var inputDir = Path.Combine(root, "content");
            var postsDir = Path.Combine(inputDir, "posts");
            Directory.CreateDirectory(postsDir);

            File.WriteAllText(Path.Combine(postsDir, "a.md"), "---\ntitle: A\ndate: 2025-12-18\nexcerpt: A\n---\n\nBody", Encoding.UTF8);
            File.WriteAllText(Path.Combine(postsDir, "b.md"), "---\ntitle: B\ndate: 2025-12-17\nexcerpt: B\n---\n\nBody", Encoding.UTF8);

            var templatesDir = Path.Combine(root, "templates");
            var assetsDir = Path.Combine(root, "assets");
            var publicDir = Path.Combine(root, "public");
            var outputDir = Path.Combine(root, "dist");

            Directory.CreateDirectory(templatesDir);
            Directory.CreateDirectory(Path.Combine(assetsDir, "css"));
            Directory.CreateDirectory(publicDir);

            File.WriteAllText(Path.Combine(templatesDir, "layout.html"), "<html><body>{{BODY}}</body></html>", Encoding.UTF8);
            File.WriteAllText(Path.Combine(templatesDir, "home.html"), "<h1>{{HOME_TITLE}}</h1>{{POST_LIST}}", Encoding.UTF8);
            File.WriteAllText(Path.Combine(templatesDir, "list.html"), "<h1>{{LIST_TITLE}}</h1>{{POST_LIST}}", Encoding.UTF8);
            File.WriteAllText(Path.Combine(templatesDir, "post.html"), "<h1>{{POST_TITLE}}</h1>{{POST_CONTENT}}", Encoding.UTF8);
            File.WriteAllText(Path.Combine(templatesDir, "about.html"), "<h1>{{ABOUT_TITLE}}</h1>{{ABOUT_CONTENT}}", Encoding.UTF8);
            File.WriteAllText(Path.Combine(templatesDir, "404.html"), "<h1>404</h1>", Encoding.UTF8);

            var options = new MarkdownBlog.Generator.GeneratorOptions(inputDir, templatesDir, assetsDir, publicDir, outputDir);
            MarkdownBlog.Generator.SiteGenerator.Generate(options);

            var indexPath = Path.Combine(outputDir, MarkdownBlog.Generator.OutputPaths.PostsIndex);
            Assert.True(File.Exists(indexPath));

            var html = File.ReadAllText(indexPath, Encoding.UTF8);
            // Each title appears twice: once for FR and once for EN (bilingual support)
            Assert.Equal(2, CountOccurrences(html, ">A<"));
            Assert.Equal(2, CountOccurrences(html, ">B<"));
        }
        finally
        {
            try { Directory.Delete(root, recursive: true); } catch { /* best-effort */ }
        }
    }

    private static int CountOccurrences(string text, string pattern)
    {
        var count = 0;
        var idx = 0;
        while (true)
        {
            idx = text.IndexOf(pattern, idx, StringComparison.Ordinal);
            if (idx < 0)
            {
                return count;
            }
            count++;
            idx += pattern.Length;
        }
    }
}
