using System.Text.Json;
using AtmMonitoring.Api;
using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class JsonSerializerContextTests
{
    [Fact]
    public void Test_AppJsonSerializerContext_Serializes_Atm()
    {
        var atm = new Atm
        {
            Id = "ATM999",
            Location = "Test Location",
            Status = AtmStatus.Online,
            CashBalance = 5000m,
            LastMaintenance = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        };

        var json = JsonSerializer.Serialize(atm, AppJsonSerializerContext.Default.Atm);

        Assert.NotNull(json);
        Assert.Contains("ATM999", json);
        Assert.Contains("Test Location", json);
    }

    [Fact]
    public void Test_AppJsonSerializerContext_Serializes_IEnumerableAtm()
    {
        var service = new AtmService();
        var atms = service.GetAllAtms();

        var json = JsonSerializer.Serialize(atms, AppJsonSerializerContext.Default.IEnumerableAtm);

        Assert.NotNull(json);
        Assert.Contains("ATM001", json);
    }
}
