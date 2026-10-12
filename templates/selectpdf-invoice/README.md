# SelectPdfInvoice

An ASP.NET Core minimal API that renders a Razor component (`Components/InvoiceDocument.razor`) to HTML and converts it to PDF with SelectPdf's Chromium engine.

```bash
dotnet run
# open http://localhost:5000/invoices/INV-1001.pdf
# (the port is printed at startup)
```

- **Layout:** `Components/InvoiceDocument.razor` holds the layout. It's plain HTML and CSS, with flexbox and grid rendered exactly as in Chrome.
- **Data:** `Invoices.cs` has the sample data. Replace `InvoiceStore` with your database.
- **Page setup:** `PdfRenderer.cs` sets the page size, the margins and the "Page X of Y" footer.

## Docker

```bash
docker build -t selectpdf-invoice .
docker run -p 8080:8080 selectpdf-invoice
# http://localhost:8080/invoices/INV-1001.pdf
```

The image works on Linux x64 and ARM64 (for example AWS Graviton). SelectPdf's native package ships the engine, so the Dockerfile has no `apt-get` lines.

## Edition

The project references either the free **SelectPdf Community Edition** (`SelectPdf.HtmlToPdf.Universal`, up to 5 pages per document) or **SelectPdf.Universal** (licensed, no page limit). You choose with `dotnet new selectpdf-invoice --edition full`. To switch later, change the package reference. Both use the `SelectPdf.Universal` namespace. With the licensed package, set `SelectPdf:LicenseKey` in configuration.

Docs: https://selectpdf.com/pdf-library/
