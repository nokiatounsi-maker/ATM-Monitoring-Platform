using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;
namespace AtmMonitoring.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
/// <summary>
/// Sealed controller class for JIT devirtualization optimization.
/// Returning domain collections directly avoids OkObjectResult wrapper allocation overhead on every HTTP request.
/// </summary>
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    [HttpGet]
    public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
