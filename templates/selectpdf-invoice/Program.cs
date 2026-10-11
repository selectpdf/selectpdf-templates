using SelectPdfInvoice;
using SelectPdfInvoice.Components;
#if (Edition == "full")
using SelectPdf.Universal;
#endif

var builder = WebApplication.CreateBuilder(args);

#if (Edition == "full")
// Your SelectPdf license key, from appsettings, user secrets or the SelectPdf__LicenseKey environment variable.
GlobalProperties.LicenseKey = builder.Configuration["SelectPdf:LicenseKey"];

#endif
builder.Services.AddSingleton<InvoiceStore>();
builder.Services.AddScoped<PdfRenderer>();

var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/invoices/INV-1001.pdf"));

// GET /invoices/INV-1001.pdf renders the Razor component Components/InvoiceDocument.razor to PDF.
app.MapGet("/invoices/{number}.pdf", async (string number, InvoiceStore store, PdfRenderer pdf, CancellationToken ct) =>
{
    Invoice? invoice = store.Find(number);
    if (invoice is null)
        return Results.NotFound();

    byte[] bytes = await pdf.RenderAsync<InvoiceDocument>(
        new Dictionary<string, object?> { [nameof(InvoiceDocument.Invoice)] = invoice }, ct);

    return Results.File(bytes, "application/pdf", $"{number}.pdf");
});

app.Run();
