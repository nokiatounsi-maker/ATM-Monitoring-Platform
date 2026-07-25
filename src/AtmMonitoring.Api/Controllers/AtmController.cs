using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
/// <summary>
/// API Controller for managing and querying ATMs.
/// This class is marked as 'sealed' to enable JIT devirtualization optimizations.
/// Sealing the controller avoids virtual dispatch overhead when resolving actions
/// and routes within ASP.NET Core framework.
/// </summary>
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    [HttpGet]
    public ActionResult<IEnumerable<Atm>> GetAll() => Ok(_atmService.GetAllAtms());
}
