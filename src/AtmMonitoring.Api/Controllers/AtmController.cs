using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

/// <summary>
/// API controller for ATM monitoring operations.
/// Sealed to enable JIT devirtualization optimizations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    [HttpGet]
    public ActionResult<IEnumerable<Atm>> GetAll() => Ok(_atmService.GetAllAtms());
}
