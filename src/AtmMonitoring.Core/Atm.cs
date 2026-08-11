namespace AtmMonitoring.Core;

/// <summary>
/// Represents an ATM entity. Sealed to enable JIT devirtualization optimizations
/// and reduce memory lookup overhead by eliminating virtual dispatch on non-inherited members.
/// </summary>
public sealed class Atm
{
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
