---
title: "First Post: Building a Static Blog"
date: 2025-12-18
excerpt: A quick tour of a tiny static blog generator with Markdown front-matter.
cover: ../../assets/img/cover.svg
tags:
  - introduction
  - static-site
---

## Why static?

Static pages are fast, simple to host, and work great with JavaScript disabled.

## A small code sample

```c#
using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace MarkdownBlog.Generator;

public static class MarkdownConverter
{
    private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .DisableHtml()
        .Build();

    public static string ToHtml(string markdown)
    {
        if (string.IsNullOrWhiteSpace(markdown))
        {
            return string.Empty;
        }

        var writer = new StringWriter();
        var renderer = new HtmlRenderer(writer);
        
        // First, let the pipeline set up the renderer
        Pipeline.Setup(renderer);
        
        // Then replace the default code block renderer with our custom one
        var codeBlockRenderer = renderer.ObjectRenderers.FindExact<CodeBlockRenderer>();
        if (codeBlockRenderer != null)
        {
            renderer.ObjectRenderers.Remove(codeBlockRenderer);
        }
        renderer.ObjectRenderers.Add(new CustomCodeBlockRenderer());
        
        var document = Markdown.Parse(markdown, Pipeline);
        renderer.Render(document);
        writer.Flush();
        
        return writer.ToString();
    }
}

public class CustomCodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
{
    protected override void Write(HtmlRenderer renderer, CodeBlock obj)
    {
        if (obj is FencedCodeBlock fencedCodeBlock)
        {
            var language = fencedCodeBlock.Info ?? string.Empty;
            var code = ExtractCode(obj);
            var lines = code.Split('\n');
            
            renderer.Write("<div class=\"code-block-container\">");
            renderer.Write("<div class=\"code-block-header\">");
            
            if (!string.IsNullOrWhiteSpace(language))
            {
                renderer.Write($"<span class=\"code-block-language\">{System.Net.WebUtility.HtmlEncode(language)}</span>");
            }
            
            renderer.Write("<button class=\"code-block-copy\" aria-label=\"Copy code to clipboard\" title=\"Copy code\">");
            renderer.Write("<svg width=\"16\" height=\"16\" viewBox=\"0 0 16 16\" fill=\"none\" xmlns=\"http://www.w3.org/2000/svg\">");
            renderer.Write("<path d=\"M4 2H3C2.44772 2 2 2.44772 2 3V13C2 13.5523 2.44772 14 3 14H10C10.5523 14 11 13.5523 11 13V12\" stroke=\"currentColor\" stroke-width=\"1.5\" stroke-linecap=\"round\"/>");
            renderer.Write("<rect x=\"6\" y=\"2\" width=\"8\" height=\"10\" rx=\"1\" stroke=\"currentColor\" stroke-width=\"1.5\"/>");
            renderer.Write("</svg>");
            renderer.Write("</button>");
            
            renderer.Write("</div>");
            
            renderer.Write("<div class=\"code-block-content\">");
            renderer.Write("<div class=\"code-block-lines\">");
            
            for (int i = 1; i <= lines.Length; i++)
            {
                renderer.Write($"<span class=\"line-number\">{i}</span>");
            }
            
            renderer.Write("</div>");
            renderer.Write("<pre><code");
            
            if (!string.IsNullOrWhiteSpace(language))
            {
                renderer.Write($" class=\"language-{System.Net.WebUtility.HtmlEncode(language)}\"");
            }
            
            renderer.Write(">");
            renderer.WriteEscape(code);
            renderer.Write("</code></pre>");
            renderer.Write("</div>");
            renderer.Write("</div>");
        }
        else
        {
            // Handle non-fenced code blocks (indented code)
            var code = ExtractCode(obj);
            renderer.Write("<pre><code>");
            renderer.WriteEscape(code);
            renderer.Write("</code></pre>");
        }
    }
    
    private static string ExtractCode(CodeBlock codeBlock)
    {
        var code = string.Empty;
        if (codeBlock.Lines.Lines != null)
        {
            var lines = codeBlock.Lines;
            var slices = new List<string>();
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines.Lines[i];
                slices.Add(line.Slice.ToString());
            }
            code = string.Join("\n", slices);
        }
        return code;
    }
}

```

## An image

![Cover placeholder for the blog](../../assets/img/cover.svg)

That’s it for the first post.
