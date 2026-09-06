using System.Text.Json;
using AtmMonitoring.Api;
using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class JsonSerializationTests
{
    [Fact]
    public void TestAtmSerializationWithSourceGenerator()
    {
        var atm = new Atm
        {
            Id = "ATM999",
            Location = "Downtown",
            Status = AtmStatus.Online,
            CashBalance = 50000.50m,
            LastMaintenance = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        };

        var options = AppJsonSerializerContext.Default.Options;

        var json = JsonSerializer.Serialize(atm, options);
        Assert.Contains("\"id\":\"ATM999\"", json);
        Assert.Contains("\"location\":\"Downtown\"", json);

        var deserialized = JsonSerializer.Deserialize<Atm>(json, options);
        Assert.NotNull(deserialized);
        Assert.Equal("ATM999", deserialized.Id);
        Assert.Equal("Downtown", deserialized.Location);
        Assert.Equal(AtmStatus.Online, deserialized.Status);
    }

    [Fact]
    public void TestAtmCollectionSerializationWithSourceGenerator()
    {
        var atms = new List<Atm>
        {
            new Atm { Id = "ATM001", Location = "Branch A", Status = AtmStatus.Online },
            new Atm { Id = "ATM002", Location = "Branch B", Status = AtmStatus.Offline }
        };

        var options = AppJsonSerializerContext.Default.Options;

        var json = JsonSerializer.Serialize<IEnumerable<Atm>>(atms, options);
        Assert.Contains("\"id\":\"ATM001\"", json);
        Assert.Contains("\"id\":\"ATM002\"", json);
    }
}
