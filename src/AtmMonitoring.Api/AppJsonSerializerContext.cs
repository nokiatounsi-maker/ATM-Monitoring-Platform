using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

/// <summary>
/// Source-generated JsonSerializerContext for System.Text.Json.
/// Using source generation generates metadata and serialization logic at compile-time,
/// eliminating runtime reflection overhead and reducing heap allocations during JSON serialization.
/// </summary>
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(List<Atm>))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
