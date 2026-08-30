using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

// Sealed class enables JIT compiler devirtualization optimizations.
[ApiController]
[Route("api/[controller]")]
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;
    public AtmController(IAtmService atmService) => _atmService = atmService;

    [HttpGet]
    public ActionResult<IEnumerable<Atm>> GetAll() => Ok(_atmService.GetAllAtms());
}
