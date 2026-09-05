using System.Collections.Generic;
using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

// Using System.Text.Json source generation eliminates runtime reflection and associated metadata allocations during JSON serialization/deserialization, improving HTTP request throughput and startup performance.
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(List<Atm>))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
