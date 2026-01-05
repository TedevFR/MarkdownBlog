using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace MarkdownBlog.Generator;

public static partial class Slugifier
{
    private static readonly Regex NonSlugChars = NonSlugCharsRegex();
    private static readonly Regex MultiHyphen = MultiHyphenRegex();

    public static string Slugify(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "post";
        }

        var normalized = input.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(normalized.Length);

        foreach (var ch in normalized)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(ch);
            if (category == UnicodeCategory.NonSpacingMark)
            {
                continue;
            }

            sb.Append(ch);
        }

        var deAccented = sb.ToString().Normalize(NormalizationForm.FormC);
        var cleaned = NonSlugChars.Replace(deAccented, "-");
        cleaned = MultiHyphen.Replace(cleaned, "-").Trim('-');

        return string.IsNullOrWhiteSpace(cleaned) ? "post" : cleaned;
    }

    [GeneratedRegex("[^a-z0-9]+", RegexOptions.Compiled)]
    private static partial Regex NonSlugCharsRegex();

    [GeneratedRegex("-+", RegexOptions.Compiled)]
    private static partial Regex MultiHyphenRegex();
}
