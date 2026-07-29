using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class AtmServiceTests
{
    [Fact]
    public void TestGetAllAtms_ReturnsInitialAtm()
    {
        var service = new AtmService();
        var atms = service.GetAllAtms();
        Assert.NotEmpty(atms);

        var list = atms.ToList();
        Assert.Single(list);
        Assert.Equal("ATM001", list[0].Id);
    }

    [Fact]
    public void TestGetAtmById_ReturnsCorrectAtm()
    {
        var service = new AtmService();
        var atm = service.GetAtmById("ATM001");
        Assert.NotNull(atm);
        Assert.Equal("ATM001", atm.Id);
    }

    [Fact]
    public void TestUpdateAtmStatus_UpdatesStatusCorrectly()
    {
        var service = new AtmService();
        service.UpdateAtmStatus("ATM001", AtmStatus.Maintenance);
        var atm = service.GetAtmById("ATM001");
        Assert.NotNull(atm);
        Assert.Equal(AtmStatus.Maintenance, atm.Status);
    }
}
