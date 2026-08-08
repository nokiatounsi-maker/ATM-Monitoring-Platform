using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

/// <summary>
/// Source-generated JsonSerializerContext to eliminate reflection overhead and heap allocations during JSON serialization.
/// </summary>
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(AtmStatus))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
