using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

/// <summary>
/// Controller handling ATM API requests.
/// Sealed to allow JIT devirtualization optimizations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    /// <summary>
    /// Returns all ATMs directly to avoid creating OkObjectResult wrapper allocation on every request.
    /// </summary>
    [HttpGet]
    public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
