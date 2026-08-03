using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

/// <summary>
/// API Controller for retrieving and managing ATM status.
/// Marked as sealed to enable JIT devirtualization optimizations.
/// Sealing the controller class allows the runtime to optimize MVC controller activation and method routing dispatch.
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
