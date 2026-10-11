using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SelectPdf.Universal;

namespace SelectPdfInvoice;

/// <summary>Renders a Razor component to HTML, then converts the HTML to PDF with SelectPdf.</summary>
public sealed class PdfRenderer(IServiceProvider services, ILoggerFactory loggerFactory)
{
    // A4, half-inch margins. SelectPdf measures in points (72 per inch).
    const int Margin = 36;
    const float PageWidth = 595.28f;

    public async Task<byte[]> RenderAsync<TComponent>(Dictionary<string, object?> parameters, CancellationToken ct = default)
        where TComponent : IComponent
    {
        string html = await RenderHtmlAsync<TComponent>(parameters);

        var converter = new HtmlToPdf();
        converter.Options.PdfPageSize = PdfPageSize.A4;
        converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
        converter.Options.MarginTop = Margin;
        converter.Options.MarginBottom = Margin;
        converter.Options.MarginLeft = Margin;
        converter.Options.MarginRight = Margin;

        // "Page 1 of 2" footer; SelectPdf fills in the variables on every page.
        converter.Options.DisplayFooter = true;
        converter.Footer.Height = 20;
        var pageNumber = new PdfTextSection(0, 6, PageWidth - 2 * Margin, "Page {page_number} of {total_pages}",
            PdfFont.CreateStandardFont(PdfStandardFont.Helvetica, 8));
        pageNumber.HorizontalAlign = PdfTextHorizontalAlign.Right;
        converter.Footer.Add(pageNumber);

        PdfDocument doc = await converter.ConvertHtmlStringAsync(html, ct);
        try
        {
            return doc.Save();
        }
        finally
        {
            doc.Close();
        }
    }

    async Task<string> RenderHtmlAsync<TComponent>(Dictionary<string, object?> parameters) where TComponent : IComponent
    {
        await using var renderer = new HtmlRenderer(services, loggerFactory);
        return await renderer.Dispatcher.InvokeAsync(async () =>
        {
            var output = await renderer.RenderComponentAsync<TComponent>(ParameterView.FromDictionary(parameters));
            return output.ToHtmlString();
        });
    }
}
