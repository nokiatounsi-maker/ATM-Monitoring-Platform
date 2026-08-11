using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;

namespace AtmMonitoring.Api.Controllers;

/// <summary>
/// API Controller for retrieving ATM information. Sealed to allow JIT devirtualization
/// and avoid virtual method table lookups.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    /// <summary>
    /// Returns the collection of ATMs directly to avoid ActionResult wrapper allocation costs
    /// on every HTTP request.
    /// </summary>
    [HttpGet]
    public IEnumerable<Atm> GetAll() => _atmService.GetAllAtms();
}
