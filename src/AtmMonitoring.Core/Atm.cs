namespace AtmMonitoring.Core;

// Sealed class to enable JIT devirtualization optimizations and prevent inheritance overhead
public sealed class Atm
{
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
