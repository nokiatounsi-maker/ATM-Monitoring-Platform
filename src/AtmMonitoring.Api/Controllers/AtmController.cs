using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
/// <summary>
/// API controller for ATM monitoring operations.
/// Sealed to enable JIT devirtualization optimizations.
/// </summary>
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    /// <summary>
    /// Gets all ATMs.
    /// Performance optimization: Returning IEnumerable&lt;Atm&gt; directly (instead of wrapping
    /// in OkObjectResult/ActionResult) avoids heap allocation of the wrapper on every HTTP request.
    /// </summary>
    [HttpGet]
    public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
