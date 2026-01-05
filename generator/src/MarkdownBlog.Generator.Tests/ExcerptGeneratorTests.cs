namespace MarkdownBlog.Generator.Tests;

using Xunit;

public sealed class ExcerptGeneratorTests
{
    [Fact]
    public void Generate_UsesFrontMatterExcerpt_WhenProvided()
    {
        var excerpt = MarkdownBlog.Generator.ExcerptGenerator.Generate("  Hello world  ", "# Ignored");
        Assert.Equal("Hello world", excerpt);
    }

    [Fact]
    public void Generate_DerivesFromFirstParagraph_AndTruncatesAtWordBoundary()
    {
        var markdown = "This is a long paragraph that should be trimmed to a reasonable length without cutting in the middle of a word. It keeps going and going.";
        var excerpt = MarkdownBlog.Generator.ExcerptGenerator.Generate(null, markdown, maxLength: 60);

        Assert.True(excerpt.Length <= 61); // includes ellipsis
        Assert.EndsWith("…", excerpt);
        Assert.DoesNotContain("`", excerpt);
    }
}
