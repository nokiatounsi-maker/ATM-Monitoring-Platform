using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;
namespace AtmMonitoring.Api.Controllers;
[ApiController] [Route("api/[controller]")]
// Sealed to enable JIT devirtualization optimizations.
public sealed class AtmController : ControllerBase {
    private readonly IAtmService _atmService;
    public AtmController(IAtmService atmService) => _atmService = atmService;
    [HttpGet] public ActionResult<IEnumerable<Atm>> GetAll() => Ok(_atmService.GetAllAtms());
}
