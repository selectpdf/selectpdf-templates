# SelectPdfConsole

Converts a URL or a local HTML file to PDF with SelectPdf's Chromium engine.

```bash
dotnet run -- https://example.com example.pdf
dotnet run -- page.html page.pdf      # relative CSS and images next to page.html are loaded
```

Runs on Windows, Linux x64/ARM64 and macOS on Apple Silicon. The project references either the free **SelectPdf Community Edition** (up to 5 pages per document) or **SelectPdf.Universal** (licensed, no page limit, set `SELECTPDF_LICENSE_KEY`), depending on `--edition`.

Docs: https://selectpdf.com/pdf-library/
