using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

/// <summary>
/// Source generator context for JSON serialization.
/// Eliminates reflection overhead and runtime heap allocations during serialization
/// by generating serialization metadata at compile time.
/// </summary>
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(List<Atm>))]
[JsonSerializable(typeof(Atm[]))]
[JsonSerializable(typeof(AtmStatus))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
