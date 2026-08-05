namespace AtmMonitoring.Core;

/// <summary>
/// Represents an Automated Teller Machine (ATM) in the platform.
/// PERFORMANCE OPTIMIZATION: This class is marked as `sealed` to allow the JIT compiler to perform devirtualization
/// optimizations. This eliminates virtual method call dispatch overhead and enables direct calls or inlining.
/// </summary>
public sealed class Atm
{
    public string Id { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public AtmStatus Status { get; set; }
    public decimal CashBalance { get; set; }
    public DateTime LastMaintenance { get; set; }
}
