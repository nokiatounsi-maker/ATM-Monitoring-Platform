using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
// Sealing core controllers enables JIT devirtualization optimizations,
// eliminating virtual dispatch overhead and allowing the compiler to perform better inlining.
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;
    public AtmController(IAtmService atmService) => _atmService = atmService;

    [HttpGet]
    public ActionResult<IEnumerable<Atm>> GetAll() => Ok(_atmService.GetAllAtms());
}
