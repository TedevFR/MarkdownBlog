namespace MarkdownBlog.Generator.Tests;

using Xunit;

public sealed class FrontMatterParserTests
{
    [Fact]
    public void Parse_ExtractsYamlAndBody()
    {
        var markdown = "---\n" +
            "title: First Post\n" +
            "date: 2025-12-18\n" +
            "excerpt: Hello there\n" +
            "tags:\n" +
            "  - one\n" +
            "  - two\n" +
            "---\n\n" +
            "# Heading\n\nContent.";

        var result = MarkdownBlog.Generator.FrontMatterParser.Parse(markdown);

        Assert.Equal("First Post", MarkdownBlog.Generator.FrontMatterParser.GetString(result.Data, "title"));
        Assert.Equal("Hello there", MarkdownBlog.Generator.FrontMatterParser.GetString(result.Data, "excerpt"));

        var dt = MarkdownBlog.Generator.FrontMatterParser.GetDateTimeOffset(result.Data, "date");
        Assert.True(dt.HasValue);

        var tags = MarkdownBlog.Generator.FrontMatterParser.GetStringArray(result.Data, "tags");
        Assert.Equal(new[] { "one", "two" }, tags);

        Assert.Contains("# Heading", result.BodyMarkdown);
        Assert.DoesNotContain("title:", result.BodyMarkdown);
    }
}
