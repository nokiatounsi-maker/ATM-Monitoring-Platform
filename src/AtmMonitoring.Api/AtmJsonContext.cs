using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

// System.Text.Json Source Generator context.
// By using source generation, reflection at runtime is eliminated, reducing memory allocations
// and improving JSON serialization/deserialization throughput in Web API responses.
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(List<Atm>))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class AtmJsonContext : JsonSerializerContext
{
}
