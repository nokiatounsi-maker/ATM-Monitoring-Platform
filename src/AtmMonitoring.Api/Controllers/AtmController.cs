using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
/// <summary>
/// API Controller for handling ATM-related operations.
/// PERFORMANCE OPTIMIZATION: This class is marked as `sealed` to allow the JIT compiler to perform devirtualization
/// optimizations. ASP.NET Core controllers benefit from devirtualized calls during routing and request invocation.
/// </summary>
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    [HttpGet]
    public ActionResult<IEnumerable<Atm>> GetAll() => Ok(_atmService.GetAllAtms());
}
