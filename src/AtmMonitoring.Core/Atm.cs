namespace AtmMonitoring.Core;

/// <summary>
/// Represents an Automated Teller Machine (ATM) in the platform.
/// Marked as sealed to enable JIT devirtualization optimizations.
/// </summary>
public sealed class Atm
{
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
