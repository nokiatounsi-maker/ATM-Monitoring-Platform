using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
// Sealed class to enable JIT devirtualization optimizations
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    // Returning IEnumerable<Atm> directly avoids creating an OkObjectResult wrapper allocation
    [HttpGet]
    public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
