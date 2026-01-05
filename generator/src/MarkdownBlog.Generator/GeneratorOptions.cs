namespace MarkdownBlog.Generator;

public readonly record struct GeneratorOptions(
    string InputDir,
    string TemplatesDir,
    string AssetsDir,
    string PublicDir,
    string OutputDir);
