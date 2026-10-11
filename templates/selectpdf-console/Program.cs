using SelectPdf.Universal;

#if (Edition == "full")
// Your SelectPdf license key.
GlobalProperties.LicenseKey = Environment.GetEnvironmentVariable("SELECTPDF_LICENSE_KEY");

#endif
if (args.Length < 1)
{
    Console.Error.WriteLine("Usage: dotnet run -- <url-or-html-file> [output.pdf]");
    Console.Error.WriteLine("Example: dotnet run -- https://example.com example.pdf");
    return 1;
}

string input = args[0];
string output = args.Length > 1 ? args[1] : "output.pdf";

var converter = new HtmlToPdf();
converter.Options.PdfPageSize = PdfPageSize.A4;
converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

// A local file is converted with its folder as base URL, so relative CSS and images load.
PdfDocument doc = File.Exists(input)
    ? converter.ConvertHtmlString(File.ReadAllText(input), new Uri(Path.GetFullPath(input)).AbsoluteUri)
    : converter.ConvertUrl(input);
try
{
    doc.Save(output);
}
finally
{
    doc.Close();
}

int pages = converter.ConversionResult.PdfPageCount;
Console.WriteLine($"Saved {Path.GetFullPath(output)} ({pages} page{(pages == 1 ? "" : "s")})");
#if (Edition == "community")
if (pages >= 5)
    Console.WriteLine("Note: the free SelectPdf Community Edition renders up to 5 pages per document. SelectPdf.Universal has no page limit.");
#endif
return 0;
