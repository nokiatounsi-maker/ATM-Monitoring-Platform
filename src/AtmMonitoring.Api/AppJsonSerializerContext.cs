using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

// Using System.Text.Json Source Generation eliminates reflection overhead
// and heap allocations for type metadata during JSON serialization/deserialization.
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(List<Atm>))]
[JsonSerializable(typeof(AtmStatus))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
