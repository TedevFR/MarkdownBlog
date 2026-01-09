using System.Reflection;

namespace MarkdownBlog.Generator;

internal static class Program
{
    public static int Main(string[] args)
    {
        if (args.Contains("--help", StringComparer.OrdinalIgnoreCase) || args.Contains("-h", StringComparer.OrdinalIgnoreCase))
        {
            PrintUsage();
            return 0;
        }

        if (!TryParseArgs(args, out var options, out var error))
        {
            Console.Error.WriteLine(error);
            Console.Error.WriteLine();
            PrintUsage();
            return 2;
        }

        try
        {
            SiteGenerator.Generate(options);
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.ToString());
            return 1;
        }
    }

    private static void PrintUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  dotnet run -- [--input <contentDir>] [--templates <templatesDir>] [--assets <assetsDir>] [--public <publicDir>] [--out <distDir>]");
        Console.WriteLine();
        Console.WriteLine("Defaults (when omitted):");
        Console.WriteLine("  --input     ./content");
        Console.WriteLine("  --templates ./site/templates");
        Console.WriteLine("  --assets    ./site/assets");
        Console.WriteLine("  --public    ./site/public");
        Console.WriteLine("  --out       ./dist");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  --input       Root content directory (expects posts under ./posts)");
        Console.WriteLine("  --templates   Directory containing HTML templates");
        Console.WriteLine("  --assets      Directory containing assets (css/js/img)");
        Console.WriteLine("  --public      Directory containing public files (favicon/robots placeholders)");
        Console.WriteLine("  --out         Output directory (dist)");
    }

    private static bool TryParseArgs(string[] args, out GeneratorOptions options, out string error)
    {
        var appPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        string? input = Path.Combine(appPath, "content");
        string? templates = Path.Combine(appPath, "site", "templates");
        string? assets = Path.Combine(appPath, "site", "assets");
        string? pub = Path.Combine(appPath, "site", "public");
        string? output = Path.Combine(appPath, "dist");

        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];

            var split = arg.Split('=', 2);
            var key = split[0];
            var value = split.Length == 2 ? split[1] : null;

            string? NextValue()
            {
                if (value is not null)
                {
                    return value;
                }

                if (i + 1 >= args.Length)
                {
                    return null;
                }

                i++;
                return args[i];
            }

            switch (key)
            {
                case "--input":
                    input = NextValue();
                    break;
                case "--templates":
                    templates = NextValue();
                    break;
                case "--assets":
                    assets = NextValue();
                    break;
                case "--public":
                    pub = NextValue();
                    break;
                case "--out":
                    output = NextValue();
                    break;
                default:
                    // Allow running with no args; otherwise flag unknown switches.
                    if (key.StartsWith("-", StringComparison.Ordinal))
                    {
                        error = $"Unknown argument: {key}";
                        options = default;
                        return false;
                    }
                    break;
            }
        }

        if (string.IsNullOrWhiteSpace(input)
            || string.IsNullOrWhiteSpace(templates)
            || string.IsNullOrWhiteSpace(assets)
            || string.IsNullOrWhiteSpace(pub)
            || string.IsNullOrWhiteSpace(output))
        {
            error = "Missing required arguments.";
            options = default;
            return false;
        }

        options = new GeneratorOptions(
            InputDir: Path.GetFullPath(input),
            TemplatesDir: Path.GetFullPath(templates),
            AssetsDir: Path.GetFullPath(assets),
            PublicDir: Path.GetFullPath(pub),
            OutputDir: Path.GetFullPath(output));

        if (args.Length == 0)
        {
            Console.WriteLine("No CLI args provided; using defaults:");
            Console.WriteLine($"  --input {options.InputDir}");
            Console.WriteLine($"  --templates {options.TemplatesDir}");
            Console.WriteLine($"  --assets {options.AssetsDir}");
            Console.WriteLine($"  --public {options.PublicDir}");
            Console.WriteLine($"  --out {options.OutputDir}");
            Console.WriteLine();
        }

        error = string.Empty;
        return true;
    }
}
