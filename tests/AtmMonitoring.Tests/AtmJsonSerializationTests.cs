using System.Text.Json;
using AtmMonitoring.Api;
using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class AtmJsonSerializationTests
{
    [Fact]
    public void TestAtmSerializationWithSourceGeneratorContext()
    {
        var atm = new Atm
        {
            Id = "ATM999",
            Location = "Downtown",
            Status = AtmStatus.Online,
            CashBalance = 50000.50m,
            LastMaintenance = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc)
        };

        var json = JsonSerializer.Serialize(atm, AppJsonSerializerContext.Default.Atm);

        Assert.NotNull(json);
        Assert.Contains("\"id\":\"ATM999\"", json);
        Assert.Contains("\"location\":\"Downtown\"", json);
        Assert.Contains("\"status\":0", json);
        Assert.Contains("\"cashBalance\":50000.5", json);

        var deserializedAtm = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.Atm);
        Assert.NotNull(deserializedAtm);
        Assert.Equal(atm.Id, deserializedAtm.Id);
        Assert.Equal(atm.Location, deserializedAtm.Location);
        Assert.Equal(atm.Status, deserializedAtm.Status);
        Assert.Equal(atm.CashBalance, deserializedAtm.CashBalance);
    }

    [Fact]
    public void TestIEnumerableAtmSerializationWithSourceGeneratorContext()
    {
        IEnumerable<Atm> atms = new List<Atm>
        {
            new Atm { Id = "ATM1", Location = "Loc1", Status = AtmStatus.Online },
            new Atm { Id = "ATM2", Location = "Loc2", Status = AtmStatus.Maintenance }
        };

        var json = JsonSerializer.Serialize(atms, AppJsonSerializerContext.Default.IEnumerableAtm);

        Assert.NotNull(json);
        Assert.Contains("\"id\":\"ATM1\"", json);
        Assert.Contains("\"id\":\"ATM2\"", json);
    }
}
