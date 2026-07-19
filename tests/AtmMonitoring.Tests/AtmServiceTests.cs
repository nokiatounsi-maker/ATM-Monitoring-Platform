using AtmMonitoring.Core;
using Xunit;
using System.Linq;

namespace AtmMonitoring.Tests;

public class AtmServiceTests {
    [Fact]
    public void Test1() {
        var service = new AtmService();
        Assert.NotEmpty(service.GetAllAtms());
    }

    [Fact]
    public void GetAllAtms_ReturnsAllAtmsCorrectly() {
        var service = new AtmService();
        var atms = service.GetAllAtms().ToList();

        Assert.Single(atms);
        Assert.Equal("ATM001", atms[0].Id);
        Assert.Equal("Main Street", atms[0].Location);
    }

    [Fact]
    public void Classes_AreSealedForDevirtualization() {
        Assert.True(typeof(Atm).IsSealed, "Atm class must be sealed for JIT devirtualization.");
        Assert.True(typeof(AtmService).IsSealed, "AtmService class must be sealed for JIT devirtualization.");
    }
}
