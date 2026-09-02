using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;
namespace AtmMonitoring.Api.Controllers;
[ApiController] [Route("api/[controller]")]
// Sealing class enables JIT devirtualization optimizations.
public sealed class AtmController : ControllerBase {
    private readonly IAtmService _atmService;
    public AtmController(IAtmService atmService) => _atmService = atmService;

    // Returning IEnumerable<Atm> directly eliminates OkObjectResult allocation per request.
    [HttpGet] public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
