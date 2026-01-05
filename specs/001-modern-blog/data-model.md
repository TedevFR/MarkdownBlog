# Data Model

## Entities

### Post
- id: derived (slug)
- title: string (1..120)
- slug: string (kebab-case, unique)
- date: datetime (ISO 8601)
- excerpt: string (0..300) — from front-matter or derived
- contentMarkdownPath: string (path under `content/posts`)
- contentHtml: string (generated)
- coverImage: string (relative path, optional)
- tags: string[] (optional)
- canonicalUrl: string (generated, relative)

Validation rules:
- title required; slug unique; date required; if future-dated, exclude from listings until `date <= now()`.
- excerpt trimmed; HTML stripped when derived; coverImage path must be within site assets.

### Page
- key: enum { about }
- title: string
- contentHtml: string (hand-authored or generated from Markdown)
- canonicalUrl: string (relative)

### NavigationLink
- label: string
- href: string (relative URL)
- position: header | footer

## Relationships
- Navigation is global and static; Post is independent content entity.

## State Transitions (Post)
- authored (markdown exists) → published (generated to `dist/` with `date <= now()`)
- authored with `date > now()` → scheduled (not included in lists until date passes)

## Derived Fields
- `slug`: from `title` via kebab-case; remove punctuation; collapse whitespace.
- `excerpt`: from front-matter; else first paragraph, 160 chars word-boundary.
- `canonicalUrl`: `/posts/{slug}`.
