using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

// Sealed to enable JIT devirtualization optimizations.
[ApiController]
[Route("api/[controller]")]
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    // Returning IEnumerable<Atm> directly avoids OkObjectResult wrapper allocation per request.
    [HttpGet]
    public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
