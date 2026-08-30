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
        Assert.Contains(atms, a => a.Id == "ATM001");
    }

    [Fact]
    public void GetAtmById_ExistingId_ReturnsAtm()
    {
        var service = new AtmService();
        var atm = service.GetAtmById("ATM001");
        Assert.NotNull(atm);
        Assert.Equal("ATM001", atm.Id);
        Assert.Equal("Main Street", atm.Location);
    }

    [Fact]
    public void GetAtmById_NonExistingId_ReturnsNull()
    {
        var service = new AtmService();
        var atm = service.GetAtmById("NON_EXISTING");
        Assert.Null(atm);
    }

    [Fact]
    public void UpdateAtmStatus_ExistingId_UpdatesStatus()
    {
        var service = new AtmService();
        service.UpdateAtmStatus("ATM001", AtmStatus.Maintenance);
        var atm = service.GetAtmById("ATM001");
        Assert.NotNull(atm);
        Assert.Equal(AtmStatus.Maintenance, atm.Status);
    }
}
