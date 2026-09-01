using System.Text.Json.Serialization;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api;

// System.Text.Json Source Generator context eliminates reflection overhead during JSON serialization.
[JsonSerializable(typeof(IEnumerable<Atm>))]
[JsonSerializable(typeof(Atm))]
[JsonSerializable(typeof(AtmStatus))]
internal partial class AppJsonContext : JsonSerializerContext
{
}
