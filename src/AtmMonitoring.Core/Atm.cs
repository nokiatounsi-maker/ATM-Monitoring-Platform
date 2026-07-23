namespace AtmMonitoring.Core;

/// <summary>
/// Represents an ATM entity.
/// This class is marked as sealed to enable JIT devirtualization optimizations,
/// preventing the overhead of virtual call dispatch when the runtime resolves types.
/// </summary>
public sealed class Atm
{
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
