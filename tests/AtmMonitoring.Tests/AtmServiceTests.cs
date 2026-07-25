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
    public void TestGetAllAtms_RetrievesAllActiveAtms() {
        var service = new AtmService();
        var atms = service.GetAllAtms();

        Assert.Single(atms);
        var firstAtm = atms.First();
        Assert.Equal("ATM001", firstAtm.Id);
        Assert.Equal("Main Street", firstAtm.Location);
        Assert.Equal(AtmStatus.Online, firstAtm.Status);
    }
}
