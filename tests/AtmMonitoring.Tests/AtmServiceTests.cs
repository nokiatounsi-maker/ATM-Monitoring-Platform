using System.Linq;
using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class AtmServiceTests
{
    [Fact]
    public void GetAllAtms_ReturnsInitialAtmCorrectly()
    {
        // Arrange
        var service = new AtmService();

        // Act
        var atms = service.GetAllAtms().ToList();

        // Assert
        Assert.Single(atms);
        var initialAtm = atms.First();
        Assert.Equal("ATM001", initialAtm.Id);
        Assert.Equal("Main Street", initialAtm.Location);
        Assert.Equal(AtmStatus.Online, initialAtm.Status);
    }

    [Fact]
    public void GetAtmById_WithValidId_ReturnsAtm()
    {
        // Arrange
        var service = new AtmService();

        // Act
        var atm = service.GetAtmById("ATM001");

        // Assert
        Assert.NotNull(atm);
        Assert.Equal("ATM001", atm!.Id);
    }

    [Fact]
    public void GetAtmById_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var service = new AtmService();

        // Act
        var atm = service.GetAtmById("INVALID");

        // Assert
        Assert.Null(atm);
    }

    [Fact]
    public void UpdateAtmStatus_UpdatesStatusSuccessfully()
    {
        // Arrange
        var service = new AtmService();

        // Act
        service.UpdateAtmStatus("ATM001", AtmStatus.Maintenance);
        var atm = service.GetAtmById("ATM001");

        // Assert
        Assert.NotNull(atm);
        Assert.Equal(AtmStatus.Maintenance, atm!.Status);
    }
}
