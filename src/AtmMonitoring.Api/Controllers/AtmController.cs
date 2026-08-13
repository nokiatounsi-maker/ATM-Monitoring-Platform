using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
/// <summary>
/// sealed to enable JIT devirtualization optimizations.
/// </summary>
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    [HttpGet]
    /// <summary>
    /// Returns IEnumerable<Atm> directly to eliminate the allocation of the OkObjectResult wrapper
    /// on every HTTP request.
    /// </summary>
    public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
