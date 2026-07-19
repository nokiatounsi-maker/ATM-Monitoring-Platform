namespace AtmMonitoring.Core;

/// <summary>
/// Represents an Automated Teller Machine (ATM) in the system.
/// This class is sealed to enable JIT devirtualization optimizations.
/// Sealing the class allows the JIT compiler to inline and optimize virtual method/property calls,
/// reducing the overhead of virtual dispatch and type-check operations.
/// </summary>
public sealed class Atm
{
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
