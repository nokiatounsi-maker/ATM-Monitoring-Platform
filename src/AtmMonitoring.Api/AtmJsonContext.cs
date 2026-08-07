using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

/// <summary>
/// Pre-compiled System.Text.Json source generation context for ATM entities.
/// Bypasses reflection-based JSON serialization at runtime, reducing both CPU overhead
/// and managed memory allocations during HTTP serialization in the API.
/// </summary>
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(Atm))]
public partial class AtmJsonContext : JsonSerializerContext
{
}
