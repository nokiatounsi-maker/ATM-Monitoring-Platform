using AtmMonitoring.Api.Controllers;
using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class AtmServiceTests
{
    [Fact]
    public void GetAllAtms_ReturnsAtms()
    {
        var service = new AtmService();
        var atms = service.GetAllAtms();
        Assert.NotEmpty(atms);
    }

    [Fact]
    public void GetAtmById_ReturnsCorrectAtm()
    {
        var service = new AtmService();
        var atm = service.GetAtmById("ATM001");
        Assert.NotNull(atm);
        Assert.Equal("ATM001", atm.Id);
    }

    [Fact]
    public void UpdateAtmStatus_UpdatesStatus()
    {
        var service = new AtmService();
        service.UpdateAtmStatus("ATM001", AtmStatus.Maintenance);
        var atm = service.GetAtmById("ATM001");
        Assert.NotNull(atm);
        Assert.Equal(AtmStatus.Maintenance, atm.Status);
    }

    [Fact]
    public void AtmController_GetAll_ReturnsCollectionDirectly()
    {
        var service = new AtmService();
        var controller = new AtmController(service);
        var result = controller.GetAll();
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}
