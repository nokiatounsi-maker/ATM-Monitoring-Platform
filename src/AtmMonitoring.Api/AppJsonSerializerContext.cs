using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

// Source Generator context for System.Text.Json serialization.
// Using source generation eliminates reflection overhead and reduces runtime heap allocations during API responses.
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(AtmStatus))]
public sealed partial class AppJsonSerializerContext : JsonSerializerContext
{
}
