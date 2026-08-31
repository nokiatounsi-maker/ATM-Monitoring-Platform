using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;
namespace AtmMonitoring.Api.Controllers;
[ApiController] [Route("api/[controller]")]
// Sealed class enables JIT devirtualization optimizations.
public sealed class AtmController : ControllerBase {
    private readonly IAtmService _atmService;
    public AtmController(IAtmService atmService) => _atmService = atmService;
    // Returning IEnumerable<Atm> directly avoids OkObjectResult wrapper allocations on every HTTP request.
    [HttpGet] public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
