namespace AtmMonitoring.Core;

/// <summary>
/// Represents an Automated Teller Machine (ATM) unit.
/// Sealed to enable JIT devirtualization optimizations and avoid virtual call dispatch overhead.
/// </summary>
public sealed class Atm {
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
