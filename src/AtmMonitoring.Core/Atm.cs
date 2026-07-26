namespace AtmMonitoring.Core;

/// <summary>
/// Represents an ATM entity.
/// Making this class 'sealed' allows the JIT compiler to perform devirtualization optimizations,
/// improving performance by skipping virtual method dispatch overhead.
/// </summary>
public sealed class Atm
{
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
