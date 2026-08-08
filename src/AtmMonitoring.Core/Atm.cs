namespace AtmMonitoring.Core;

// Sealing core domain classes enables JIT devirtualization optimizations,
// eliminating virtual dispatch overhead and allowing the compiler to perform better inlining.
public sealed class Atm
{
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
