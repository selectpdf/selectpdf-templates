# SelectPdf.Templates

`dotnet new` templates for generating PDFs from HTML in .NET with SelectPdf's Chromium engine.

```bash
dotnet new install SelectPdf.Templates

dotnet new selectpdf-invoice -o MyInvoices     # ASP.NET Core API: Razor invoice to PDF, with Dockerfile
dotnet new selectpdf-console -o HtmlToPdf      # console app: URL or HTML file to PDF
```

| Template | Short name | What you get |
|---|---|---|
| PDF Invoice Web API (SelectPdf) | `selectpdf-invoice` | Minimal API, `GET /invoices/{number}.pdf`, a Razor component invoice layout, a "Page X of Y" footer, and a Dockerfile for Linux x64/ARM64 with no `apt-get` |
| HTML to PDF Console App (SelectPdf) | `selectpdf-console` | `dotnet run -- <url-or-file> [output.pdf]` |

## Options

| Option | Values | Default |
|---|---|---|
| `--edition` | `community`: free SelectPdf Community Edition, HTML to PDF, up to 5 pages per document<br>`full`: SelectPdf.Universal (licensed), no page limit, merging, PDF/UA, Word to PDF | `community` |
| `--framework` | `net10.0`, `net8.0` | `net10.0` |

The generated project picks SelectPdf's native engine package for the machine that builds it, whether Windows, Linux x64/ARM64 or macOS on Apple Silicon. Publishing with `-r <rid>` uses that platform instead.

Docs: https://selectpdf.com/pdf-library/ · MIT licensed.
