using System.Text.Json;
using AtmMonitoring.Api;
using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class JsonSerializerContextTests
{
    [Fact]
    public void AppJsonSerializerContext_SerializesAtmCorrectly()
    {
        var atm = new Atm
        {
            Id = "ATM999",
            Location = "Downtown Branch",
            Status = AtmStatus.Online,
            CashBalance = 50000m,
            LastMaintenance = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        };

        var json = JsonSerializer.Serialize(atm, AppJsonSerializerContext.Default.Atm);

        Assert.NotNull(json);
        Assert.Contains("\"id\":\"ATM999\"", json);
        Assert.Contains("\"location\":\"Downtown Branch\"", json);
        Assert.Contains("\"status\":0", json);

        var deserializedAtm = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.Atm);
        Assert.NotNull(deserializedAtm);
        Assert.Equal(atm.Id, deserializedAtm.Id);
        Assert.Equal(atm.Location, deserializedAtm.Location);
        Assert.Equal(atm.Status, deserializedAtm.Status);
    }

    [Fact]
    public void AppJsonSerializerContext_SerializesAtmCollectionCorrectly()
    {
        var atms = new List<Atm>
        {
            new Atm { Id = "ATM001", Location = "Location 1", Status = AtmStatus.Online },
            new Atm { Id = "ATM002", Location = "Location 2", Status = AtmStatus.Offline }
        };

        var json = JsonSerializer.Serialize((IEnumerable<Atm>)atms, AppJsonSerializerContext.Default.IEnumerableAtm);

        Assert.NotNull(json);
        Assert.Contains("\"id\":\"ATM001\"", json);
        Assert.Contains("\"id\":\"ATM002\"", json);
    }
}
