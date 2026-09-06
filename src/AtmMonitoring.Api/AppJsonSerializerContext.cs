using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

/// <summary>
/// Source-generated JsonSerializerContext to eliminate reflection overhead
/// and reduce memory allocations during JSON serialization in ASP.NET Core endpoints.
/// </summary>
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(List<Atm>))]
[JsonSerializable(typeof(AtmStatus))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
