using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

// System.Text.Json source generator context to eliminate reflection overhead and heap allocations during JSON serialization.
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(AtmStatus))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
