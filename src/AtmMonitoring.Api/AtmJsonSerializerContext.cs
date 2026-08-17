using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

/// <summary>
/// Source generator context for System.Text.Json serialization.
/// Eliminates runtime reflection overhead and reduces memory allocations during JSON serialization.
/// </summary>
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(AtmStatus))]
public partial class AtmJsonSerializerContext : JsonSerializerContext
{
}
