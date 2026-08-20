using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
// Sealed class enables JIT devirtualization
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    /// <summary>
    /// Returning IEnumerable directly avoids wrapping the response in OkObjectResult, saving heap allocation on every HTTP request.
    /// </summary>
    [HttpGet]
    public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
