namespace AtmMonitoring.Core;

/// <summary>
/// Core model representing an ATM.
/// This class is sealed to allow the JIT compiler to devirtualize virtual calls
/// on this type, reducing call overhead and enabling further optimizations like inlining.
/// </summary>
public sealed class Atm
{
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
