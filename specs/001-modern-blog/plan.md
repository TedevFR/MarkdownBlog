# Implementation Plan: Modern Blog Core Pages

**Branch**: `001-modern-blog` | **Date**: 2025-12-18 | **Spec**: [specs/001-modern-blog/spec.md](specs/001-modern-blog/spec.md)
**Input**: Feature specification from `/specs/001-modern-blog/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

Deliver a static, modern blog with Home (latest posts), All Posts index, About, and individual Post pages. Content is authored in Markdown with front-matter and converted to static HTML via a C# 14 (.NET 10) console generator. The site is static-first, responsive/mobile-ready, accessible, SEO-friendly, and functions with JavaScript disabled.

## Technical Context

<!--
  ACTION REQUIRED: Replace the content in this section with the technical details
  for the project. The structure here is presented in advisory capacity to guide
  the iteration process.
-->

**Language/Version**: Frontend: HTML5, CSS3, ES6+; Generator: C# 14 on .NET 10 LTS  
**Primary Dependencies**: Generator: Markdig (Markdown → HTML), YamlDotNet (front-matter), optional: NUglify (CSS/JS minify)  
**Storage**: Files only (Markdown source, assets, generated static files)  
**Testing**: Lighthouse CI (perf/SEO/a11y), link checker, unit tests for generator with xUnit (NEEDS CLARIFICATION on test scope)  
**Target Platform**: Static hosting (any CDN/static host), modern desktop/mobile browsers  
**Project Type**: Web (static site + generator CLI)  
**Performance Goals**: Lighthouse ≥ A11y 90, SEO 90, Best Practices 90, Performance 85 (SC-004); visible content ≤1s desktop/≤2s 4G (SC-005)  
**Constraints**: Static-first; core content renders without JS; relative URLs; subpath-safe; CSP-compatible (no inline scripts/styles)  
**Scale/Scope**: Initial content: ≥2 posts; no pagination; low traffic; future growth supported by generator

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- Static-First: All pages generated to static HTML/CSS/JS. STATUS: PASS
- Structure: Provide index.html, 404.html, /assets, /public, /dist. STATUS: PASS
- Accessibility: Semantic HTML, keyboard navigation, visible focus, contrast AA. STATUS: PASS (TO ENFORCE IN TEMPLATES)
- Performance: One CSS and one JS (deferred/module), compress assets, lazy-load images. STATUS: PASS
- SEO/Share: Unique title/description/canonical; OG tags on key pages. STATUS: PASS
- Security Hygiene: No secrets; no inline scripts/styles (CSP-friendly). STATUS: PASS
- Routing: Relative URLs; subpath-safe; friendly 404. STATUS: PASS
- Build/Deploy: Versioned assets; sensible cache headers (host-config). STATUS: PASS

## Project Structure

### Documentation (this feature)

```text
specs/[###-feature]/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)
<!--
  ACTION REQUIRED: Replace the placeholder tree below with the concrete layout
  for this feature. Delete unused options and expand the chosen structure with
  real paths (e.g., apps/admin, packages/something). The delivered plan must
  not include Option labels.
-->

```text
content/
└── posts/                       # Markdown sources with front-matter

generator/                       # C# 14 console app
└── src/
  ├── MarkdownBlog.Generator/  # CLI project
  └── MarkdownBlog.Generator.Tests/ (optional)

site/                            # Author-maintained static assets and templates
├── templates/                   # HTML templates (layout, post, list, about)
├── assets/
│   ├── css/
│   ├── js/
│   └── img/
└── public/                      # robots.txt, sitemap.xml (generated), favicon.ico

dist/                            # Build output (final site), served by host
```

**Structure Decision**: Static site with a .NET-based generator. Source content in `content/`, templates/assets in `site/`, generator in `generator/`, build output in `dist/`. Aligns with Constitution directories (`/assets`, `/public`, `/dist`).

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| [e.g., 4th project] | [current need] | [why 3 projects insufficient] |
| [e.g., Repository pattern] | [specific problem] | [why direct DB access insufficient] |
