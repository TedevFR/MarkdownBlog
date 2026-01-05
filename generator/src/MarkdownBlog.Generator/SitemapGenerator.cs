using System.Text;

namespace MarkdownBlog.Generator;

public static class SitemapGenerator
{
    public static void WriteRobotsAndSitemap(string distPublicDir, IEnumerable<string> hrefs)
    {
        Directory.CreateDirectory(distPublicDir);

        var sitemapPath = Path.Combine(distPublicDir, "sitemap.xml");
        var robotsPath = Path.Combine(distPublicDir, "robots.txt");

        File.WriteAllText(sitemapPath, BuildSitemapXml(hrefs), Encoding.UTF8);
        File.WriteAllText(robotsPath, "User-agent: *\nAllow: /\nSitemap: sitemap.xml\n", Encoding.UTF8);
    }

    public static string BuildSitemapXml(IEnumerable<string> hrefs)
    {
        var sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
        sb.Append("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n");

        foreach (var href in hrefs.Where(h => !string.IsNullOrWhiteSpace(h)))
        {
            sb.Append("  <url><loc>");
            sb.Append(EscapeXml(href.Trim()));
            sb.Append("</loc></url>\n");
        }

        sb.Append("</urlset>\n");
        return sb.ToString();
    }

    private static string EscapeXml(string s)
    {
        return s
            .Replace("&", "&amp;", StringComparison.Ordinal)
            .Replace("<", "&lt;", StringComparison.Ordinal)
            .Replace(">", "&gt;", StringComparison.Ordinal)
            .Replace("\"", "&quot;", StringComparison.Ordinal)
            .Replace("'", "&apos;", StringComparison.Ordinal);
    }
}
