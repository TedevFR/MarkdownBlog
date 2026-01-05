using Markdig;

namespace MarkdownBlog.Generator;

public static class MarkdownConverter
{
    private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .DisableHtml()
        .Build();

    public static string ToHtml(string markdown)
    {
        if (string.IsNullOrWhiteSpace(markdown))
        {
            return string.Empty;
        }

        return Markdown.ToHtml(markdown, Pipeline);
    }
}
