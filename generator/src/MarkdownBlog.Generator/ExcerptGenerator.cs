using System.Text.RegularExpressions;

namespace MarkdownBlog.Generator;

public static partial class ExcerptGenerator
{
    public const int DefaultMaxLength = 160;
    public const int MaxStoredLength = 300;

    public static string Generate(string? frontMatterExcerpt, string markdownBody, int maxLength = DefaultMaxLength)
    {
        if (!string.IsNullOrWhiteSpace(frontMatterExcerpt))
        {
            return TrimToLength(frontMatterExcerpt.Trim(), Math.Min(MaxStoredLength, maxLength));
        }

        var paragraph = ExtractFirstParagraph(markdownBody);
        var plain = MarkdownToPlainText(paragraph);
        if (string.IsNullOrWhiteSpace(plain))
        {
            return string.Empty;
        }

        return TrimToLength(plain, maxLength);
    }

    private static string ExtractFirstParagraph(string markdown)
    {
        if (string.IsNullOrWhiteSpace(markdown))
        {
            return string.Empty;
        }

        var normalized = markdown.Replace("\r\n", "\n").Trim();
        var parts = Regex.Split(normalized, @"\n\s*\n");
        foreach (var p in parts)
        {
            var trimmed = p.Trim();
            if (!string.IsNullOrWhiteSpace(trimmed))
            {
                return trimmed;
            }
        }

        return string.Empty;
    }

    private static string MarkdownToPlainText(string markdown)
    {
        if (string.IsNullOrWhiteSpace(markdown))
        {
            return string.Empty;
        }

        var s = markdown;

        // Remove fenced code blocks
        s = FencedCodeBlockRegex().Replace(s, " ");

        // Strip headings/blockquote markers
        s = LinePrefixRegex().Replace(s, "");

        // Strip inline code markers
        s = s.Replace("`", string.Empty);

        // Strip emphasis markers
        s = s.Replace("*", string.Empty).Replace("_", string.Empty);

        // Convert links [text](url) -> text
        s = MarkdownLinkRegex().Replace(s, "$1");

        // Convert images ![alt](src) -> alt
        s = MarkdownImageRegex().Replace(s, "$1");

        // Collapse whitespace
        s = WhitespaceRegex().Replace(s, " ").Trim();

        return s;
    }

    private static string TrimToLength(string text, int maxLength)
    {
        var normalized = WhitespaceRegex().Replace(text, " ").Trim();
        if (normalized.Length <= maxLength)
        {
            return normalized;
        }

        var cut = normalized[..maxLength];
        var lastSpace = cut.LastIndexOf(' ');
        if (lastSpace > 60)
        {
            cut = cut[..lastSpace];
        }

        return cut.TrimEnd('.', ',', ';', ':', ' ') + "…";
    }

    [GeneratedRegex(@"(?ms)^```.*?^```\s*$", RegexOptions.Compiled)]
    private static partial Regex FencedCodeBlockRegex();

    [GeneratedRegex(@"(?m)^(?:\s{0,3}(?:#{1,6}|>|\*|-|\+|\d+\.)\s+)", RegexOptions.Compiled)]
    private static partial Regex LinePrefixRegex();

    [GeneratedRegex("!\\[([^\\]]*)\\]\\([^\\)]*\\)", RegexOptions.Compiled)]
    private static partial Regex MarkdownImageRegex();

    [GeneratedRegex("\\[([^\\]]+)\\]\\([^\\)]*\\)", RegexOptions.Compiled)]
    private static partial Regex MarkdownLinkRegex();

    [GeneratedRegex("\\s+", RegexOptions.Compiled)]
    private static partial Regex WhitespaceRegex();
}
