---

description: "Tasks for implementing Modern Blog Core Pages"
---

# Tasks: Modern Blog Core Pages

**Input**: Design documents from `/specs/001-modern-blog/`
**Prerequisites**: plan.md (required), spec.md (required for user stories), research.md, data-model.md, contracts/

**Tests**: Tests are included where requested by spec/research.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3)
- Include exact file paths in descriptions

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [x] T001 Create directories: content/posts, generator/src/MarkdownBlog.Generator, generator/src/MarkdownBlog.Generator.Tests, site/templates, site/assets/css, site/assets/js, site/assets/img, site/public, dist
- [x] T002 Initialize .NET console app in generator/src/MarkdownBlog.Generator (create project and solution)
- [x] T003 [P] Add NuGet dependencies Markdig and YamlDotNet to generator/src/MarkdownBlog.Generator
- [x] T004 [P] Create base CSS and JS placeholders in site/assets/css/main.css and site/assets/js/main.js
- [x] T005 [P] Create base HTML templates in site/templates/layout.html, site/templates/post.html, site/templates/home.html, site/templates/list.html, site/templates/about.html, site/templates/404.html
- [x] T006 Initialize xUnit test project in generator/src/MarkdownBlog.Generator.Tests (project setup)

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [ ] T007 Create core entities in generator/src/MarkdownBlog.Generator/Post.cs, generator/src/MarkdownBlog.Generator/Page.cs, generator/src/MarkdownBlog.Generator/NavigationLink.cs
- [ ] T008 [P] Implement front-matter parser with YamlDotNet in generator/src/MarkdownBlog.Generator/FrontMatterParser.cs
- [ ] T009 [P] Implement Markdown → HTML converter with Markdig in generator/src/MarkdownBlog.Generator/MarkdownConverter.cs
- [ ] T010 [P] Implement slugification utility in generator/src/MarkdownBlog.Generator/Slugifier.cs
- [ ] T011 Implement excerpt generator in generator/src/MarkdownBlog.Generator/ExcerptGenerator.cs
- [ ] T012 Implement generator CLI argument parsing in generator/src/MarkdownBlog.Generator/Program.cs (supports --input, --templates, --assets, --public, --out)
- [ ] T013 [P] Implement template renderer (token replacement) in generator/src/MarkdownBlog.Generator/TemplateRenderer.cs
- [ ] T014 [P] Implement asset/public copier to dist in generator/src/MarkdownBlog.Generator/AssetPipeline.cs
- [ ] T015 Implement post filtering (exclude future-dated) in generator/src/MarkdownBlog.Generator/PostFilter.cs
- [ ] T016 Implement relative URL builder and canonical URL in generator/src/MarkdownBlog.Generator/UrlBuilder.cs
- [ ] T017 [P] Implement sitemap and robots generation in generator/src/MarkdownBlog.Generator/SitemapGenerator.cs (outputs to dist/public)
- [ ] T018 [P] Wire global navigation in site/templates/layout.html (links: /, /posts, /about; relative URLs)
- [ ] T019 Configure build output structure to dist/index.html, dist/posts/index.html, dist/posts/{slug}/index.html, dist/about/index.html, dist/404.html

- [x] T007 Create core entities in generator/src/MarkdownBlog.Generator/Post.cs, generator/src/MarkdownBlog.Generator/Page.cs, generator/src/MarkdownBlog.Generator/NavigationLink.cs
- [x] T008 [P] Implement front-matter parser with YamlDotNet in generator/src/MarkdownBlog.Generator/FrontMatterParser.cs
- [x] T009 [P] Implement Markdown → HTML converter with Markdig in generator/src/MarkdownBlog.Generator/MarkdownConverter.cs
- [x] T010 [P] Implement slugification utility in generator/src/MarkdownBlog.Generator/Slugifier.cs
- [x] T011 Implement excerpt generator in generator/src/MarkdownBlog.Generator/ExcerptGenerator.cs
- [x] T012 Implement generator CLI argument parsing in generator/src/MarkdownBlog.Generator/Program.cs (supports --input, --templates, --assets, --public, --out)
- [x] T013 [P] Implement template renderer (token replacement) in generator/src/MarkdownBlog.Generator/TemplateRenderer.cs
- [x] T014 [P] Implement asset/public copier to dist in generator/src/MarkdownBlog.Generator/AssetPipeline.cs
- [x] T015 Implement post filtering (exclude future-dated) in generator/src/MarkdownBlog.Generator/PostFilter.cs
- [x] T016 Implement relative URL builder and canonical URL in generator/src/MarkdownBlog.Generator/UrlBuilder.cs
- [x] T017 [P] Implement sitemap and robots generation in generator/src/MarkdownBlog.Generator/SitemapGenerator.cs (outputs to dist/public)
- [x] T018 [P] Wire global navigation in site/templates/layout.html (links: /, /posts, /about; relative URLs)
- [x] T019 Configure build output structure to dist/index.html, dist/posts/index.html, dist/posts/{slug}/index.html, dist/about/index.html, dist/404.html

**Checkpoint**: Foundation ready — user story implementation can now begin in parallel

---

## Phase 3: User Story 1 — Read a blog post (Priority: P1) 🎯 MVP

**Goal**: A visitor opens a post URL and reads full content with clean typography and metadata.

**Independent Test**: Open dist/posts/{slug}/index.html directly; verify readable content, title, date, description; renders with JavaScript disabled.

### Tests for User Story 1 (requested)

- [x] T020 [P] [US1] Unit test slugging in generator/src/MarkdownBlog.Generator.Tests/SlugifierTests.cs
- [x] T021 [P] [US1] Unit test front-matter parsing in generator/src/MarkdownBlog.Generator.Tests/FrontMatterParserTests.cs
- [x] T022 [P] [US1] Unit test excerpt generation in generator/src/MarkdownBlog.Generator.Tests/ExcerptGeneratorTests.cs
- [x] T023 [P] [US1] Integration test generates a post page with title/date/content in generator/src/MarkdownBlog.Generator.Tests/Integration/PostPageTests.cs

### Implementation for User Story 1

- [x] T024 [P] [US1] Create two sample posts in content/posts/first-post.md and content/posts/second-post.md (with YAML front-matter)
- [x] T025 [P] [US1] Implement post page generation in generator/src/MarkdownBlog.Generator/PostPageGenerator.cs (outputs dist/posts/{slug}/index.html)
- [x] T026 [US1] Implement post template with tokens in site/templates/post.html (title, date, content)
- [x] T027 [US1] Add metadata (title, description, canonical URL) in site/templates/layout.html for post pages
- [x] T028 [US1] Ensure images and code blocks render accessibly (alt text, semantics) via MarkdownConverter pipeline in generator/src/MarkdownBlog.Generator/MarkdownConverter.cs
- [x] T029 [US1] Verify pages render core content without JS (no inline scripts/styles) using site/templates/layout.html

**Checkpoint**: User Story 1 is fully functional and independently testable

---

## Phase 4: User Story 2 — See latest posts on Home (Priority: P1)

**Goal**: Home page shows latest posts with title, date, excerpt, and link.

**Independent Test**: Open dist/index.html and confirm latest posts are shown in descending date order with working links.

### Tests for User Story 2 (requested)

- [x] T030 [P] [US2] Integration test verifies latest posts listing in generator/src/MarkdownBlog.Generator.Tests/Integration/HomePageTests.cs
- [x] T031 [P] [US2] Contract test: ensure dist/index.html exists and validates core elements in generator/src/MarkdownBlog.Generator.Tests/Contract/HomeRouteTests.cs

### Implementation for User Story 2

- [x] T032 [P] [US2] Implement home page generation in generator/src/MarkdownBlog.Generator/HomePageGenerator.cs (outputs dist/index.html)
- [x] T033 [US2] Implement home listing template in site/templates/home.html (title, date, excerpt, read-more)
- [x] T034 [US2] Ensure excerpt truncation (~160 chars, word boundary) applied for listings in generator/src/MarkdownBlog.Generator/ExcerptGenerator.cs
- [x] T035 [US2] Add Open Graph tags for home page in site/templates/layout.html

**Checkpoint**: User Stories 1 AND 2 work independently

---

## Phase 5: User Story 3 — Browse all posts list (Priority: P2)

**Goal**: All Posts page lists every post with dates, sorted by most recent first.

**Independent Test**: Open dist/posts/index.html and verify all posts appear once, sorted by date descending, with links working.

### Tests for User Story 3 (requested)

- [x] T036 [P] [US3] Integration test verifies full posts index in generator/src/MarkdownBlog.Generator.Tests/Integration/PostsIndexTests.cs
- [x] T037 [P] [US3] Contract test: ensure dist/posts/index.html exists and validates core elements in generator/src/MarkdownBlog.Generator.Tests/Contract/PostsRouteTests.cs

### Implementation for User Story 3

- [x] T038 [P] [US3] Implement posts index generation in generator/src/MarkdownBlog.Generator/PostsIndexGenerator.cs (outputs dist/posts/index.html)
- [x] T039 [US3] Implement posts list template in site/templates/list.html (title, date, link)
- [x] T040 [US3] Handle empty state in site/templates/list.html (no posts message)

**Checkpoint**: All user stories are independently functional

---

## Phase N: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [x] T041 [P] Improve accessibility in site/templates/layout.html and site/assets/css/main.css (semantic landmarks, focus styles, contrast AA)
- [ ] T042 [P] Optional minification pipeline with NUglify in generator/src/MarkdownBlog.Generator/AssetPipeline.cs
- [x] T043 Add unique titles/descriptions and canonical links for all pages in site/templates/layout.html
- [x] T044 [P] Generate robots.txt and sitemap.xml in dist/public via generator/src/MarkdownBlog.Generator/SitemapGenerator.cs
- [x] T045 [P] Run link checker across dist/ via a simple script or generator/src/MarkdownBlog.Generator/LinkChecker.cs
- [x] T046 Code cleanup and documentation updates in specs/001-modern-blog/quickstart.md

---

## Dependencies & Execution Order

### Phase Dependencies

- Setup (Phase 1): No dependencies — start immediately
- Foundational (Phase 2): Depends on Setup — BLOCKS all user stories
- User Stories (Phase 3+): Depend on Foundational completion
  - US1 (P1) and US2 (P1) can proceed in parallel
  - US3 (P2) proceeds after foundation; independent from US1/US2
- Polish: Depends on desired user stories completion

### User Story Dependencies

- User Story 1 (P1): Starts after Foundational — no dependency on other stories
- User Story 2 (P1): Starts after Foundational — independent; may reuse US1 components
- User Story 3 (P2): Starts after Foundational — independent; may reuse US1 components

### Within Each User Story

- Tests (if included) SHOULD be written and FAIL before implementation
- Models before services; services before page generation; templates finalized before integration

### Parallel Opportunities

- Setup tasks marked [P] can run in parallel
- Foundational tasks marked [P] can run in parallel
- After Foundational, US1 and US2 can run in parallel
- Within a story, tests and models marked [P] can run in parallel

## Parallel Execution Examples

- [US1]
  - T020, T021, T022, T023 can run together
  - T024 and T025 can run together; T026–T028 can proceed once template/layout exist
- [US2]
  - T030 and T031 can run together
  - T032 and T033 can run together
- [US3]
  - T036 and T037 can run together
  - T038 and T039 can run together

## Implementation Strategy

**MVP First (User Story 1 Only)**

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational (CRITICAL)
3. Complete Phase 3: User Story 1
4. STOP and VALIDATE: Test User Story 1 independently
5. Demo/deploy static output

**Incremental Delivery**

1. Setup + Foundational → Foundation ready
2. Add US1 → Test independently → Demo
3. Add US2 → Test independently → Demo
4. Add US3 → Test independently → Demo

