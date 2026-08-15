using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

/// <summary>
/// System.Text.Json Source Generator context for AtmMonitoring.Api.
/// Generating JSON metadata at compile-time eliminates reflection overhead, lowers startup time,
/// and reduces per-request heap allocations during API response serialization.
/// </summary>
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(List<Atm>))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
