# Quickstart

## Prerequisites
- .NET 10 SDK (for C# 14)
- Any static host or local server for preview (optional: `dotnet-serve`)

## Project Layout
- content/posts: Markdown sources with YAML front-matter
- generator: .NET console app to build the site
- site/templates: HTML templates (layout, post, list, about)
- site/assets: CSS/JS/images (relative URLs)
- site/public: robots.txt, favicon.ico; generator writes sitemap.xml
- dist: Build output

## Build the Site
```powershell
# From repo root
# Restore and build generator
dotnet build .\generator\MarkdownBlog.sln

# Run generator (assumes CLI like: dotnet run -- --input ../content --templates ../site/templates --assets ../site/assets --public ../site/public --out ../dist)
pushd generator
 dotnet run -- --input "..\content" --templates "..\site\templates" --assets "..\site\assets" --public "..\site\public" --out "..\dist"
popd
```

Notes:
- The generator writes pages to `dist/` and copies `site/assets` to `dist/assets` and `site/public` to `dist/public`.
- A basic internal link check runs after generation and will fail if any relative `href`/`src` targets are missing.

## Preview Locally
```powershell
# Option 1: dotnet-serve (install once)
dotnet tool install -g dotnet-serve

dotnet-serve -d .\dist -o
```

## Add a Post
1. Create `content/posts/my-post.md` with YAML front-matter:
```markdown
---
title: My Post
date: 2025-12-18
excerpt: Short summary of the post.
cover: assets/img/cover.jpg
---

# Heading

Your content here.
```
2. Re-run the generator.

## Quality Checks
- Run Lighthouse on Home and a Post page: target A11y ≥ 90, SEO ≥ 90, Best Practices ≥ 90, Performance ≥ 85.
- Verify no broken internal links; ensure 404.html works.
- Confirm pages render core content with JavaScript disabled.
