# MarkdownBlog

A fast, minimal static blog generator built with .NET 10 that converts Markdown posts into a fully functional static website.

## Overview

MarkdownBlog is a static site generator designed for simplicity, performance, and accessibility. It takes Markdown files with YAML front-matter and generates a complete static website with clean HTML, proper SEO.

## Features

- 📝 **Markdown to HTML**: Converts Markdown posts using Markdig with advanced extensions
- 🎨 **YAML Front-Matter**: Supports metadata (title, date, excerpt, tags, cover image)
- 🚀 **Static Output**: Generates pure HTML/CSS/JS - fast, secure, and easy to host
- 🔍 **SEO Optimized**: Includes meta tags, Open Graph tags, canonical URLs, sitemap, and robots.txt
- ♿ **Accessible**: Semantic HTML, works without JavaScript, AA contrast compliance
- 🎯 **Smart Features**: 
  - Automatic slug generation
  - Code blocks with syntax highlighting support
  - Relative URL resolution
  - Link validation

## Architecture

### Project Structure

```
MarkdownBlog/
├── generator/
│   └── src/
│       ├── MarkdownBlog.Generator/          # Main generator project
│       │   ├── Program.cs                    # CLI entry point
│       │   ├── SiteGenerator.cs             # Main orchestrator
│       │   ├── Post.cs                      # Post model
│       │   ├── PostLoader.cs                # Loads posts from Markdown
│       │   ├── PostFilter.cs                # Filters publishable posts
│       │   ├── FrontMatterParser.cs         # YAML front-matter parser
│       │   ├── MarkdownConverter.cs         # Markdown to HTML converter
│       │   ├── HomePageGenerator.cs         # Home page generation
│       │   ├── PostPageGenerator.cs         # Individual post pages
│       │   ├── PostsIndexGenerator.cs       # All posts index
│       │   ├── TemplateRenderer.cs          # Token-based template engine
│       │   ├── AssetPipeline.cs             # Asset copying
│       │   ├── SitemapGenerator.cs          # SEO files generation
│       │   ├── LinkChecker.cs               # Validates internal links
│       │   ├── UrlBuilder.cs                # URL utilities
│       │   ├── Slugifier.cs                 # URL slug generation
│       │   ├── ExcerptGenerator.cs          # Post excerpt generation
│       │   ├── OpenGraph.cs                 # Open Graph meta tags
│       │   ├── content/                     # Sample content
│       │   │   └── posts/*.md               # Markdown posts
│       │   └── site/                        # Site resources
│       │       ├── templates/*.html         # HTML templates
│       │       ├── assets/                  # CSS/JS/images
│       │       └── public/                  # Static files
│       └── MarkdownBlog.Generator.Tests/    # Test project
│           ├── Integration/                 # Integration tests
│           └── Contract/                    # Contract tests
└── dist/                                    # Generated output (git-ignored)
    ├── index.html                           # Home page
    ├── about/index.html                     # About page
    ├── posts/                               # Blog posts
    │   ├── index.html                       # All posts index
    │   └── {slug}/index.html               # Individual posts
    ├── 404.html                             # Not found page
    └── public/                              # Assets and SEO files
        ├── css/main.css
        ├── js/main.js
        ├── robots.txt
        └── sitemap.xml
```

### Core Components

1. **Post Loading & Parsing**
   - `PostLoader`: Scans content directory for Markdown files
   - `FrontMatterParser`: Extracts YAML metadata using YamlDotNet
   - `MarkdownConverter`: Converts Markdown to HTML with custom code block rendering

2. **Content Processing**
   - `PostFilter`: Filters posts by publish date
   - `ExcerptGenerator`: Creates excerpts (~160 chars, word boundaries)
   - `Slugifier`: Generates URL-safe slugs from titles

3. **Page Generation**
   - `HomePageGenerator`: Latest 10 posts
   - `PostPageGenerator`: Individual post pages
   - `PostsIndexGenerator`: Complete post archive
   - Static pages: About, 404

4. **Template System**
   - `TemplateRenderer`: Simple token replacement (e.g., `{{POST_TITLE}}`)
   - Layout template with navigation and SEO meta tags
   - Page-specific templates for different content types

5. **Asset Pipeline**
   - `AssetPipeline`: Copies CSS, JavaScript, and images
   - `SitemapGenerator`: Creates robots.txt and sitemap.xml
   - `LinkChecker`: Validates internal links

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later

### Installation

1. Clone the repository:
```bash
git clone https://github.com/TedevFR/MarkdownBlog.git
cd MarkdownBlog
```

2. Build the project:
```bash
cd generator/src/MarkdownBlog.Generator
dotnet build
```

### Usage

#### Basic Usage (with defaults)

```bash
cd generator/src/MarkdownBlog.Generator
dotnet run
```

This uses the following default directories:
- Input: `./content`
- Templates: `./site/templates`
- Assets: `./site/assets`
- Public: `./site/public`
- Output: `./dist`

#### Custom Directories

```bash
dotnet run -- --input /path/to/content \
              --templates /path/to/templates \
              --assets /path/to/assets \
              --public /path/to/public \
              --out /path/to/output
```

#### Command Line Options

- `--input <dir>`: Root content directory (expects posts under `./posts`)
- `--templates <dir>`: Directory containing HTML templates
- `--assets <dir>`: Directory containing assets (CSS/JS/images)
- `--public <dir>`: Directory containing public files
- `--out <dir>`: Output directory
- `--help`, `-h`: Show usage information

## Creating Content

### Post Format

Create Markdown files in `content/posts/` with YAML front-matter:

```markdown
---
title: "Your Post Title"
date: 2025-12-18
excerpt: A brief description of your post (optional, auto-generated if missing)
cover: ../../assets/img/cover.svg
tags:
  - tag1
  - tag2
slug: custom-url-slug  # Optional, auto-generated from title
---

## Your Content Here

Write your post content in Markdown format.

### Code Blocks

\```csharp
public class Example 
{
    public string Name { get; set; }
}
\```

### Images

![Alt text](../../assets/img/image.png)
```

### Front-Matter Fields

- **title** (required): Post title
- **date** (required): Publication date (ISO format)
- **excerpt** (optional): Short description (~160 chars recommended)
- **slug** (optional): URL slug (auto-generated from title if not provided)
- **cover** (optional): Cover image path
- **tags** (optional): Array of tags

### Post Filtering

Posts with future dates are automatically excluded from the generated site. This allows you to prepare posts in advance.

## Output Structure

The generator creates a clean, SEO-friendly URL structure:

```
/                           → Home page (latest posts)
/posts/                     → All posts archive
/posts/{slug}/              → Individual post
/about/                     → About page
/404.html                   → Not found page
/public/robots.txt          → Search engine directives
/public/sitemap.xml         → Site map
/public/css/main.css        → Styles
/public/js/main.js          → Scripts
```

## Development

### Running Tests

```bash
cd generator/src/MarkdownBlog.Generator.Tests
dotnet test
```

### Project Dependencies

- **[Markdig](https://github.com/xoofx/markdig)**: Extensible Markdown processor
- **[YamlDotNet](https://github.com/aaubry/YamlDotNet)**: YAML parser for front-matter

## Design Principles

1. **Simplicity**: Minimal dependencies, straightforward architecture
2. **Performance**: Static HTML loads instantly, no database queries
3. **Accessibility**: Semantic HTML, works without JavaScript, keyboard navigable
4. **SEO-Friendly**: Proper meta tags, canonical URLs, sitemap
5. **Developer Experience**: Clear code structure, comprehensive tests

## Technology Stack

- **.NET 10**: Modern C# with latest language features
- **Markdig**: Fast and extensible Markdown processor
- **YamlDotNet**: Robust YAML parsing
- **xUnit**: Testing framework

## Deployment

The generated `dist/` folder contains everything needed for deployment. Simply upload to any static hosting service:

- GitHub Pages
- Netlify
- Vercel
- Azure Static Web Apps
- AWS S3 + CloudFront
- Any web server (Apache, Nginx)

### Automated Deployment with GitHub Pages

This repository is configured for **automatic deployment to GitHub Pages** using GitHub Actions. Every push to the `master` branch triggers a complete CI/CD pipeline.

#### How It Works

The `.github/workflows/dotnet.yml` workflow automates the entire build and deployment process:

1. **Build & Test** 
   - Sets up .NET 10.0 environment
   - Restores dependencies
   - Builds the solution in Release configuration
   - Runs all unit and integration tests

2. **Generate Static Site**
   - Executes the MarkdownBlog generator
   - Produces the complete `dist/` folder with all HTML, CSS, JS, and assets

3. **Deploy to GitHub Pages**
   - Uploads the `dist/` folder as a GitHub Pages artifact
   - Automatically deploys to your GitHub Pages site
   - Available at `https://<username>.github.io/<repository>/`

#### Workflow Triggers

- **Push to `master`**: Full build, test, and deploy
- **Pull Request**: Build and test only (no deployment)

This setup ensures your blog is always up-to-date with zero manual deployment steps needed!

## License

This project is open source. See the repository for license details.

## Author

**Teddy Le Bras**

- Blog: https://github.com/TedevFR/MarkdownBlog
- Focus: Software architecture, .NET, cloud modernization, and AI

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.
