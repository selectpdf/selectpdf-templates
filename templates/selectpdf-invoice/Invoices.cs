namespace SelectPdfInvoice;

public sealed record Party(string Name, string Address, string Email);

public sealed record InvoiceLine(string Description, decimal Quantity, decimal UnitPrice)
{
    public decimal Amount => Quantity * UnitPrice;
}

public sealed record Invoice(
    string Number, DateOnly Issued, DateOnly Due, Party Seller, Party Customer,
    IReadOnlyList<InvoiceLine> Lines, string Currency, decimal TaxRate)
{
    public decimal Subtotal => Lines.Sum(l => l.Amount);
    public decimal Tax => Math.Round(Subtotal * TaxRate, 2);
    public decimal Total => Subtotal + Tax;
}

/// <summary>Sample data. Replace with your database.</summary>
public sealed class InvoiceStore
{
    static readonly Party Seller = new("Contoso Ltd.", "1 Main Street, Springfield", "billing@contoso.example");

    readonly Dictionary<string, Invoice> invoices = new(StringComparer.OrdinalIgnoreCase)
    {
        ["INV-1001"] = new("INV-1001", new DateOnly(2026, 10, 1), new DateOnly(2026, 10, 31), Seller,
            new Party("Fabrikam Inc.", "200 Market Road, Riverside", "accounts@fabrikam.example"),
            [
                new("Consulting, October", 12, 95m),
                new("Hosting, monthly plan", 1, 49m),
                new("Support hours", 3.5m, 80m),
            ],
            "EUR", 0.19m),
    };

    public Invoice? Find(string number) => invoices.GetValueOrDefault(number);
}
