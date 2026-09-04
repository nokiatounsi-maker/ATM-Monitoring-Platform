using System.Text.Json;
using AtmMonitoring.Api;
using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class JsonSerializerContextTests
{
    [Fact]
    public void AppJsonSerializerContext_CanSerializeAndDeserialize_Atm()
    {
        var atm = new Atm
        {
            Id = "ATM999",
            Location = "Test Location",
            Status = AtmStatus.Online,
            CashBalance = 5000.50m,
            LastMaintenance = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        };

        var json = JsonSerializer.Serialize(atm, AppJsonSerializerContext.Default.Atm);
        Assert.NotNull(json);
        Assert.Contains("ATM999", json);

        var deserialized = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.Atm);
        Assert.NotNull(deserialized);
        Assert.Equal(atm.Id, deserialized.Id);
        Assert.Equal(atm.Location, deserialized.Location);
        Assert.Equal(atm.Status, deserialized.Status);
    }

    [Fact]
    public void AppJsonSerializerContext_CanSerialize_AtmEnumerable()
    {
        var service = new AtmService();
        var atms = service.GetAllAtms();

        var json = JsonSerializer.Serialize(atms, AppJsonSerializerContext.Default.IEnumerableAtm);
        Assert.NotNull(json);
        Assert.Contains("ATM001", json);
    }
}
