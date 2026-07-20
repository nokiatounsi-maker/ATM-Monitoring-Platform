namespace AtmMonitoring.Core;

/// <summary>
/// Represents an ATM entity in the monitoring system.
/// This class is marked as 'sealed' to enable JIT devirtualization optimizations.
/// Sealing the class allows the .NET compiler and RyuJIT to eliminate virtual call overhead
/// and perform aggressive inlining where applicable.
/// </summary>
public sealed class Atm
{
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
