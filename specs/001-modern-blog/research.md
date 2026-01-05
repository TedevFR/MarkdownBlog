# Research Summary

## Decisions

- Decision: Use Markdig for Markdown parsing in C#
  - Rationale: Mature, performant, extensible Markdown parser for .NET; supports CommonMark and extensions (tables, code fences). Widely adopted.
  - Alternatives considered: MarkdownSharp (stale), Lunet/Markdig pipelines (similar), external Node tools (adds Node dependency).

- Decision: Use YamlDotNet to parse front-matter
  - Rationale: Reliable YAML parser for .NET to extract `title`, `date`, `excerpt`, optional `cover`, `tags`.
  - Alternatives considered: Ad-hoc regex (fragile), JSON front-matter (less common in Markdown workflows).

- Decision: Plain HTML templates loaded from files
  - Rationale: Keep generator simple and transparent; vanilla HTML/CSS/JS requirement; no runtime engine. Insert variables via simple token replacement.
  - Alternatives considered: RazorLight (powerful but adds complexity), Scriban (templating DSL adds dependency and learning curve).

- Decision: Route structure `/`, `/posts`, `/posts/{slug}`, `/about` with `404.html`
  - Rationale: Human-readable, stable URLs; matches spec; easy to link and index.
  - Alternatives considered: `/blog` prefix (unnecessary scope), date-based paths (not required for initial scope).

- Decision: Relative URLs and subpath-safe asset links
  - Rationale: Required by spec and constitution; enables hosting under subdirectories.
  - Alternatives considered: Absolute URLs (break subpath deployments).

- Decision: Filter out future-dated posts from listings
  - Rationale: Matches edge-case requirement; only show posts with `date <= now()`.
  - Alternatives considered: Show with badge (contradicts requirement).

- Decision: Excerpt strategy
  - Rationale: Prefer `excerpt` in front-matter; else derive from first paragraph (strip HTML) and trim to ~160 chars with word boundary.
  - Alternatives considered: First N words anywhere (may include headings/captions), manual only (less resilient).

- Decision: Accessibility and semantics
  - Rationale: Semantic headings (`h1` per page), landmarks (`header`, `nav`, `main`, `footer`), visible focus, alt text, sufficient contrast (WCAG AA).
  - Alternatives considered: None acceptable.

- Decision: SEO and share metadata
  - Rationale: Unique `<title>`, `<meta name="description">`, canonical URL; Open Graph tags on posts/home/about; JSON-LD optional later.
  - Alternatives considered: Per-page defaults only (insufficient for SEO goals).

- Decision: Asset strategy: single CSS and minimal JS
  - Rationale: Performance and constitution alignment; load JS with `defer` or as module.
  - Alternatives considered: Multiple CSS/JS files (more blocking requests).

- Decision: Minification approach
  - Rationale: For small initial site, optional NUglify for CSS/JS in generator; HTML minify is optional to keep templates readable.
  - Alternatives considered: Node-based bundlers (violates dependency simplicity).

- Decision: Sitemap and robots
  - Rationale: Generator writes `public/sitemap.xml` and `public/robots.txt`; includes canonical URLs for main pages and posts.
  - Alternatives considered: Manual (error-prone), host-generated (not always available).

- Decision: Link checking and Lighthouse
  - Rationale: Add a simple link-check step in generator or separate script; use Lighthouse in CI/manual to meet SC-004.
  - Alternatives considered: No automated checks (risk of regressions).

## Clarifications Resolved

- Testing scope for generator: Provide minimal xUnit tests for slugging, front-matter parsing, and excerpt generation. More can be added later.
- Favicon and icons: Place in `site/public/`; link via relative paths.
- Image handling: Use relative URLs, add `alt` text from Markdown; consider lazy-loading with `loading="lazy"` on non-critical images.

## Open Items

- None blocking Phase 1. All NEEDS CLARIFICATION items are resolved above.
