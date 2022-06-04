using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers;

[Route("api/mvc/[controller]")]
public class AirportsController : Controller
{
    private readonly IAirportService _airportService;
    private readonly IUserManagementMvcService _userManagementMvcService;

    public AirportsController(IAirportService airportService, IUserManagementMvcService userManagementMvcService)
    {
        _airportService = airportService;
        _userManagementMvcService = userManagementMvcService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAirports()
    {
        return View(await _airportService.GetAllAirports(await HttpContext.GetTokenAsync("access_token")));
    }
}