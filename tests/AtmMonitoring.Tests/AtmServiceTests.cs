using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class AtmServiceTests
{
    [Fact]
    public void GetAllAtms_ReturnsInitialAtms()
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
        var atm = service.GetAtmById("NONEXISTENT");
        Assert.Null(atm);
    }

    [Fact]
    public void UpdateAtmStatus_ExistingAtm_UpdatesStatus()
    {
        var service = new AtmService();
        service.UpdateAtmStatus("ATM001", AtmStatus.Maintenance);
        var atm = service.GetAtmById("ATM001");
        Assert.NotNull(atm);
        Assert.Equal(AtmStatus.Maintenance, atm.Status);
    }
}
