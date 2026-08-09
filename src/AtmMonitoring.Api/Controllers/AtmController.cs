using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;
namespace AtmMonitoring.Api.Controllers;
[ApiController] [Route("api/[controller]")]
/// <summary>
/// Controller for ATM endpoints. Sealed to enable JIT devirtualization optimizations.
/// </summary>
public sealed class AtmController : ControllerBase {
    private readonly IAtmService _atmService;
    public AtmController(IAtmService atmService) => _atmService = atmService;
    [HttpGet]
    /// <summary>
    /// Gets all ATMs.
    /// Returns IEnumerable&lt;Atm&gt; directly to avoid the memory allocation overhead of OkObjectResult wrappers on every HTTP request.
    /// </summary>
    public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
