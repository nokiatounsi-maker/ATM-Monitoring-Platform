namespace AtmMonitoring.Core;

/// <summary>
/// Represents an Automated Teller Machine (ATM) entity.
/// This class is marked as 'sealed' to enable JIT devirtualization optimizations.
/// Marking classes as sealed allows the RyuJIT compiler to inline methods/properties
/// and bypass virtual dispatch overhead (e.g. devirtualization), resulting in faster execution.
/// </summary>
public sealed class Atm
{
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
