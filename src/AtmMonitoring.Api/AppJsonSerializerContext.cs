using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

// System.Text.Json Source Generation reduces reflection overhead and allocations
// during JSON serialization and deserialization in ASP.NET Core endpoints.
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(List<Atm>))]
[JsonSerializable(typeof(Atm[]))]
[JsonSerializable(typeof(AtmStatus))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
