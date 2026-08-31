using AtmMonitoring.Core;
using AtmMonitoring.Api.Controllers;
using Xunit;

namespace AtmMonitoring.Tests;

public class AtmServiceTests
{
    [Fact]
    public void GetAllAtms_ReturnsNonEmptyCollection()
    {
        var service = new AtmService();
        var atms = service.GetAllAtms();
        Assert.NotEmpty(atms);
    }

    [Fact]
    public void GetAtmById_ExistingId_ReturnsAtm()
    {
        var service = new AtmService();
        var atm = service.GetAtmById("ATM001");
        Assert.NotNull(atm);
        Assert.Equal("ATM001", atm.Id);
    }

    [Fact]
    public void GetAtmById_NonExistingId_ReturnsNull()
    {
        var service = new AtmService();
        var atm = service.GetAtmById("ATM999");
        Assert.Null(atm);
    }

    [Fact]
    public void UpdateAtmStatus_ExistingId_UpdatesStatus()
    {
        var service = new AtmService();
        service.UpdateAtmStatus("ATM001", AtmStatus.Offline);
        var atm = service.GetAtmById("ATM001");
        Assert.NotNull(atm);
        Assert.Equal(AtmStatus.Offline, atm.Status);
    }

    [Fact]
    public void AtmController_GetAll_ReturnsAtms()
    {
        var service = new AtmService();
        var controller = new AtmController(service);
        var result = controller.GetAll();
        Assert.NotEmpty(result);
    }
}
