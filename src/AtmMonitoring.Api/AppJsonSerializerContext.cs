using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

// System.Text.Json Source Generator context to eliminate reflection overhead
// and reduce heap allocations during JSON serialization in ASP.NET Core endpoints.
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(List<Atm>))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
