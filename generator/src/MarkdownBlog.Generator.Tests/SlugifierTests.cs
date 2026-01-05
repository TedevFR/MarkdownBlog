namespace MarkdownBlog.Generator.Tests;

using Xunit;

public sealed class SlugifierTests
{
    [Theory]
    [InlineData("Hello World", "hello-world")]
    [InlineData("  Hello   World  ", "hello-world")]
    [InlineData("C# & .NET: What's new?", "c-net-what-s-new")]
    [InlineData("Crème brûlée", "creme-brulee")]
    [InlineData("---", "post")]
    public void Slugify_ProducesKebabCase(string input, string expected)
    {
        var actual = MarkdownBlog.Generator.Slugifier.Slugify(input);
        Assert.Equal(expected, actual);
    }
}
