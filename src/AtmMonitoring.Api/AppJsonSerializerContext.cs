using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

/// <summary>
/// System.Text.Json source generation context to eliminate runtime reflection overhead,
/// improve throughput, and reduce GC allocations during JSON serialization.
/// </summary>
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(AtmStatus))]
public partial class AppJsonSerializerContext : JsonSerializerContext;
