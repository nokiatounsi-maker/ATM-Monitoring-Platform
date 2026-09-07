using System.Text.Json;
using AtmMonitoring.Api;
using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class AtmJsonContextTests
{
    [Fact]
    public void SerializeAtm_WithSourceGenerator_ReturnsValidJson()
    {
        var atm = new Atm
        {
            Id = "ATM999",
            Location = "Downtown",
            Status = AtmStatus.Online,
            CashBalance = 50000m,
            LastMaintenance = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        };

        string json = JsonSerializer.Serialize(atm, AtmJsonContext.Default.Atm);

        Assert.Contains("\"id\":\"ATM999\"", json);
        Assert.Contains("\"location\":\"Downtown\"", json);
        Assert.Contains("\"cashBalance\":50000", json);
    }

    [Fact]
    public void DeserializeAtm_WithSourceGenerator_ReturnsAtmObject()
    {
        string json = "{\"id\":\"ATM999\",\"location\":\"Downtown\",\"status\":0,\"cashBalance\":50000}";

        var atm = JsonSerializer.Deserialize(json, AtmJsonContext.Default.Atm);

        Assert.NotNull(atm);
        Assert.Equal("ATM999", atm.Id);
        Assert.Equal("Downtown", atm.Location);
        Assert.Equal(AtmStatus.Online, atm.Status);
        Assert.Equal(50000m, atm.CashBalance);
    }
}
