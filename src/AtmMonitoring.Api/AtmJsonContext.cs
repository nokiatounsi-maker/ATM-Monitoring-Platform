using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

/// <summary>
/// Source-generated JSON serialization context for the API.
/// This eliminates reflection overhead and reduces heap allocations during serialization and deserialization.
/// </summary>
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(AtmStatus))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(List<Atm>))]
internal partial class AtmJsonContext : JsonSerializerContext
{
}
