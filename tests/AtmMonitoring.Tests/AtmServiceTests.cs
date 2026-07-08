using AtmMonitoring.Core;
using Xunit;
namespace AtmMonitoring.Tests;
public class AtmServiceTests {
    [Fact] public void Test1() { var service = new AtmService(); Assert.NotEmpty(service.GetAllAtms()); }
}
