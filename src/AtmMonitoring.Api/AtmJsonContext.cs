using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

/// <summary>
/// Source-generated JsonSerializerContext for Atm and IEnumerable&lt;Atm&gt; serialization.
/// Eliminates runtime reflection and heap allocations during JSON serialization.
/// </summary>
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(IEnumerable<Atm>))]
internal partial class AtmJsonContext : JsonSerializerContext
{
}
