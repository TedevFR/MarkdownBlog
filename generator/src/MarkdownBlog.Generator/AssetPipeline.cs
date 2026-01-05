namespace MarkdownBlog.Generator;

public static class AssetPipeline
{
    public static void CopyAssets(string assetsDir, string publicDir, string distDir)
    {
        CopyDirectory(assetsDir, Path.Combine(distDir, "assets"));
        CopyDirectory(publicDir, Path.Combine(distDir, "public"));
    }

    public static void CopyDirectory(string sourceDir, string targetDir)
    {
        if (!Directory.Exists(sourceDir))
        {
            return;
        }

        Directory.CreateDirectory(targetDir);

        foreach (var dirPath in Directory.EnumerateDirectories(sourceDir, "*", SearchOption.AllDirectories))
        {
            var rel = Path.GetRelativePath(sourceDir, dirPath);
            Directory.CreateDirectory(Path.Combine(targetDir, rel));
        }

        foreach (var filePath in Directory.EnumerateFiles(sourceDir, "*", SearchOption.AllDirectories))
        {
            var rel = Path.GetRelativePath(sourceDir, filePath);
            var dest = Path.Combine(targetDir, rel);
            Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
            File.Copy(filePath, dest, overwrite: true);
        }
    }
}
