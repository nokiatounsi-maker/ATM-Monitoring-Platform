using Microsoft.AspNetCore.Mvc;
using AtmMonitoring.Core;
namespace AtmMonitoring.Api.Controllers;
[ApiController] [Route("api/[controller]")]
/// <summary>
/// Controller for retrieving ATM information.
/// This class is sealed to enable JIT compiler devirtualization of virtual methods/calls on this controller.
/// </summary>
public sealed class AtmController : ControllerBase {
    private readonly IAtmService _atmService;
    public AtmController(IAtmService atmService) => _atmService = atmService;
    [HttpGet] public ActionResult<IEnumerable<Atm>> GetAll() => Ok(_atmService.GetAllAtms());
}
