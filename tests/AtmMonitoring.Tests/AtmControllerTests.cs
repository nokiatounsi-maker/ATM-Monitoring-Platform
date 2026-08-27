using AtmMonitoring.Api.Controllers;
using AtmMonitoring.Core;
using Xunit;

namespace AtmMonitoring.Tests;

public class AtmControllerTests
{
    [Fact]
    public void GetAll_ReturnsAllAtmsFromService()
    {
        var service = new AtmService();
        var controller = new AtmController(service);

        var result = controller.GetAll();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}
