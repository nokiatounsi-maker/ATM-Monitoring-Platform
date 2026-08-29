namespace AtmMonitoring.Core;
// Marked class as sealed to enable JIT compiler devirtualization optimizations.
public sealed class Atm { public string Id { get; set; } = string.Empty; public string Location { get; set; } = string.Empty; public AtmStatus Status { get; set; } public decimal CashBalance { get; set; } public DateTime LastMaintenance { get; set; } }
