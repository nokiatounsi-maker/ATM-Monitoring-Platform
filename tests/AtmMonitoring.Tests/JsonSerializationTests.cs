using System.Text.Json;
using AtmMonitoring.Api;
using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class JsonSerializationTests
{
    [Fact]
    public void Atm_Serializes_With_SourceGenerator()
    {
        var atm = new Atm
        {
            Id = "ATM001",
            Location = "Main Street",
            Status = AtmStatus.Online,
            CashBalance = 50000m,
            LastMaintenance = DateTime.UnixEpoch
        };

        var json = JsonSerializer.Serialize(atm, AppJsonSerializerContext.Default.Atm);

        Assert.NotNull(json);
        Assert.Contains("ATM001", json);
        Assert.Contains("Main Street", json);

        var deserialized = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.Atm);
        Assert.NotNull(deserialized);
        Assert.Equal(atm.Id, deserialized.Id);
        Assert.Equal(atm.Location, deserialized.Location);
        Assert.Equal(atm.Status, deserialized.Status);
    }
}
