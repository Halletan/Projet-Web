using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers;

[Route("api/mvc/[controller]")]
public class AircraftsController : Controller
{
    private readonly IAircraftService _aircraftService;
    private readonly IUserManagementMvcService _userManagementMvcService;

    public AircraftsController(IAircraftService aircraftService, IUserManagementMvcService userManagementMvcService)
    {
        _aircraftService = aircraftService;
        _userManagementMvcService = userManagementMvcService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAircrafts()
    {
        TempData["Role"] = await _userManagementMvcService.GetRole(await HttpContext.GetTokenAsync("access_token"));
        return View(await _aircraftService.GetAllAircrafts(await HttpContext.GetTokenAsync("access_token")));
    }
}