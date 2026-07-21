using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
/// <summary>
/// API Controller for ATM status operations.
/// This class is sealed to enable JIT compiler devirtualization optimizations.
/// </summary>
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    [HttpGet]
    public ActionResult<IEnumerable<Atm>> GetAll() => Ok(_atmService.GetAllAtms());
}
