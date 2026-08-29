using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;
namespace AtmMonitoring.Api.Controllers;
[ApiController] [Route("api/[controller]")]
// Marked class as sealed to enable JIT compiler devirtualization optimizations.
public sealed class AtmController : ControllerBase {
    private readonly IAtmService _atmService;
    public AtmController(IAtmService atmService) => _atmService = atmService;
    // Performance Optimization: Returning IEnumerable<Atm> directly instead of wrapping in OkObjectResult
    // avoids allocation of OkObjectResult wrapper on every HTTP GET request.
    [HttpGet] public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
