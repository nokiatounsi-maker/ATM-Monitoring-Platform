using System.Text.Json;
using AtmMonitoring.Api;
using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class JsonSerializationTests
{
    [Fact]
    public void Test_AppJsonSerializerContext_CanSerialize_AtmIEnumerable()
    {
        var atms = new List<Atm>
        {
            new Atm { Id = "ATM001", Location = "Main Street", Status = AtmStatus.Online, CashBalance = 50000m }
        };

        string json = JsonSerializer.Serialize(atms, AppJsonSerializerContext.Default.IEnumerableAtm);
        Assert.NotNull(json);
        Assert.Contains("ATM001", json);
        Assert.Contains("Main Street", json);
        Assert.Contains("cashBalance", json);
    }
}
