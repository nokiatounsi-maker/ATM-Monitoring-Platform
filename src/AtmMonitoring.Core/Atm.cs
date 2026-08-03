namespace AtmMonitoring.Core;

/// <summary>
/// Represents an ATM system. Marked as sealed to enable JIT devirtualization optimizations.
/// Sealing the class allows the JIT compiler to bypass virtual method dispatch tables,
/// resulting in direct calls, smaller instruction sizes, and improved performance.
/// </summary>
public sealed class Atm
{
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
