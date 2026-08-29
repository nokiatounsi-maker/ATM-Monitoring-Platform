using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

// Performance Optimization: System.Text.Json Source Generation eliminates reflection overhead
// and heap allocations during JSON serialization.
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(List<Atm>))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
