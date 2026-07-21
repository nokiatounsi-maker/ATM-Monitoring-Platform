namespace AtmMonitoring.Core;

/// <summary>
/// Represents an ATM unit.
/// This class is sealed to allow the JIT compiler to perform devirtualization optimizations.
/// </summary>
public sealed class Atm
{
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
