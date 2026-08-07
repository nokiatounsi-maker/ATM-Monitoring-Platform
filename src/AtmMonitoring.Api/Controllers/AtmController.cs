using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
// Sealed to enable JIT devirtualization optimizations
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    // Returning IEnumerable<Atm> directly from the action instead of wrapping it in OkObjectResult
    // avoids allocating a wrapper OkObjectResult object on every API request.
    [HttpGet]
    public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
