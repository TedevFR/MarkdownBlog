# Static Web App Constitution

## Core Principles

### I. Static-First
- Deliver prebuilt files (HTML, CSS, JS, images) via static hosting.
- No databases, or SSR are required.

### II. Accessibility
- Use semantic HTML, visible focus, labels, and keyboard navigation.
- Meet WCAG 2.1 AA color contrast for text and UI controls.

### III. Performance
- Keep payloads small: minify/compress assets, lazy-load images, and defer non-critical JS.

### IV. SEO & Shareability
- Provide a unique <title> and meta description per page, plus a canonical URL.
- Include basic Open Graph tags for social sharing where applicable.

### V. Security Hygiene
- HTTPS-only; no secrets in client code or .env files.
- CSP-compatible markup (avoid inline scripts/styles); send `X-Content-Type-Options: nosniff` when hosting.

## Minimum Requirements

### Structure
- Entry: `index.html`; Fallback: `404.html` for unknown routes.
- Directories: `/assets` (css/js/img/fonts), `/public` (robots.txt, sitemap.xml, favicon.ico), `/dist` (build output).

### HTML
- `<meta charset="utf-8">` and `<meta name="viewport" content="width=device-width, initial-scale=1">`.
- One `<h1>` per page; descriptive `<title>` and `<meta name="description">`.

### CSS/JS
- Prefer one minified CSS and one minified JS bundle.
- Load non-critical JS with `defer` or as ES modules; keep CSS render-blocking minimal.

### Routing
- Use relative URLs; support subpath deployments.
- `404.html` provides a friendly fallback and link back to home.

### Build & Deploy
- Cache policy: HTML `Cache-Control: public, max-age=300`; versioned assets with content hashes `public, max-age=31536000, immutable`.

## Definition of Done
- Lighthouse: Accessibility ≥ 90, SEO ≥ 90, Best Practices ≥ 90, Performance ≥ 85.
- Core content renders with JavaScript disabled; zero console errors on load.
- No broken internal links; works on latest Chrome/Firefox/Safari/Edge (desktop and mobile).

## Governance
- These requirements take precedence for static delivery concerns.
- Amendments require documenting impact and updating deployment guidance.

**Version**: 1.0.0 | **Ratified**: 2025-12-18 | **Last Amended**: 2025-12-18
