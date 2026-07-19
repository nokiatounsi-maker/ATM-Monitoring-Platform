using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
/// <summary>
/// Controller for ATM monitoring operations.
/// This class is sealed to enable JIT devirtualization optimizations.
/// Sealing the controller avoids virtual dispatch overhead and improves startup,
/// routing, and instantiation performance in ASP.NET Core.
/// </summary>
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    [HttpGet]
    public ActionResult<IEnumerable<Atm>> GetAll() => Ok(_atmService.GetAllAtms());
}
