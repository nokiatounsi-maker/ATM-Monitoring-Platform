using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;
namespace AtmMonitoring.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
// Sealed controller enables JIT devirtualization optimizations.
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;
    public AtmController(IAtmService atmService) => _atmService = atmService;

    // Returning domain collection directly eliminates OkObjectResult wrapper allocation on every request.
    [HttpGet]
    public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
