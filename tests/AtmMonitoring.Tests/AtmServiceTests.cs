using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class AtmServiceTests
{
    [Fact]
    public void TestInitialAtmExists()
    {
        var service = new AtmService();
        var atms = service.GetAllAtms();
        Assert.NotEmpty(atms);

        var initialAtm = Assert.Single(atms);
        Assert.Equal("ATM001", initialAtm.Id);
        Assert.Equal("Main Street", initialAtm.Location);
        Assert.Equal(AtmStatus.Online, initialAtm.Status);
    }

    [Fact]
    public void TestGetAtmById()
    {
        var service = new AtmService();
        var atm = service.GetAtmById("ATM001");
        Assert.NotNull(atm);
        Assert.Equal("ATM001", atm.Id);

        var nonExistent = service.GetAtmById("ATM999");
        Assert.Null(nonExistent);
    }

    [Fact]
    public void TestUpdateAtmStatus()
    {
        var service = new AtmService();

        service.UpdateAtmStatus("ATM001", AtmStatus.Maintenance);
        var atm = service.GetAtmById("ATM001");
        Assert.NotNull(atm);
        Assert.Equal(AtmStatus.Maintenance, atm.Status);
    }

    [Fact]
    public void TestGetAllAtmsIsLazyAndAccurate()
    {
        var service = new AtmService();
        var atms = service.GetAllAtms();

        // Let's modify the status of the initial ATM
        service.UpdateAtmStatus("ATM001", AtmStatus.Offline);

        // Because of direct iteration/deferred execution, the yielded values reflect the current state
        var firstAtm = Assert.Single(atms);
        Assert.Equal(AtmStatus.Offline, firstAtm.Status);
    }
}
