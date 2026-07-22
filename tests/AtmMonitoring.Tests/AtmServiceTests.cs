using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class AtmServiceTests
{
    [Fact]
    public void GetAllAtms_ReturnsInitialAtmAndRetrievesAllAtms()
    {
        // Arrange
        var service = new AtmService();

        // Act
        var atms = service.GetAllAtms();

        // Assert
        Assert.NotEmpty(atms);
        var atmList = atms.ToList();
        Assert.Single(atmList);
        Assert.Equal("ATM001", atmList[0].Id);
    }

    [Fact]
    public void GetAtmById_ReturnsCorrectAtm_WhenIdExists()
    {
        // Arrange
        var service = new AtmService();

        // Act
        var atm = service.GetAtmById("ATM001");

        // Assert
        Assert.NotNull(atm);
        Assert.Equal("ATM001", atm.Id);
    }

    [Fact]
    public void GetAtmById_ReturnsNull_WhenIdDoesNotExist()
    {
        // Arrange
        var service = new AtmService();

        // Act
        var atm = service.GetAtmById("NONEXISTENT");

        // Assert
        Assert.Null(atm);
    }

    [Fact]
    public void UpdateAtmStatus_UpdatesStatusCorrectly()
    {
        // Arrange
        var service = new AtmService();

        // Act
        service.UpdateAtmStatus("ATM001", AtmStatus.Maintenance);
        var atm = service.GetAtmById("ATM001");

        // Assert
        Assert.NotNull(atm);
        Assert.Equal(AtmStatus.Maintenance, atm.Status);
    }
}
