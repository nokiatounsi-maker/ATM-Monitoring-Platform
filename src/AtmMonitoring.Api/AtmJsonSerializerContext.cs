using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

/// <summary>
/// Source-generated JsonSerializerContext to eliminate reflection-based JSON serialization overhead and reduce memory allocations.
/// </summary>
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(AtmStatus))]
internal partial class AtmJsonSerializerContext : JsonSerializerContext
{
}
