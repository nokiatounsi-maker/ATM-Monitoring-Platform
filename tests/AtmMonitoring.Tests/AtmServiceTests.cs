using AtmMonitoring.Core;
using Xunit;
namespace AtmMonitoring.Tests;
public class AtmServiceTests {
    [Fact]
    public void Test1() {
        var service = new AtmService();
        Assert.NotEmpty(service.GetAllAtms());
    }

    [Fact]
    public void GetAtmById_ReturnsCorrectAtm_WhenIdExists()
    {
        var service = new AtmService();
        var atm = service.GetAtmById("ATM001");
        Assert.NotNull(atm);
        Assert.Equal("ATM001", atm.Id);
    }

    [Fact]
    public void GetAtmById_ReturnsNull_WhenIdDoesNotExist()
    {
        var service = new AtmService();
        var atm = service.GetAtmById("ATM999");
        Assert.Null(atm);
    }

    [Fact]
    public void UpdateAtmStatus_CorrectlyUpdatesStatus()
    {
        var service = new AtmService();
        service.UpdateAtmStatus("ATM001", AtmStatus.Maintenance);
        var atm = service.GetAtmById("ATM001");
        Assert.NotNull(atm);
        Assert.Equal(AtmStatus.Maintenance, atm.Status);
    }
}
