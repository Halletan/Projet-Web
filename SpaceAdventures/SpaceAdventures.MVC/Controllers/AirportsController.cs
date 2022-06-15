using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers;

[Route("api/mvc/[controller]")]
public class AirportsController : Controller
{
    private readonly IAirportService _airportService;
    private readonly IPlanetService _planetService;
    private readonly IUserManagementMvcService _userManagementMvcService;

    public AirportsController(IAirportService airportService, IUserManagementMvcService userManagementMvcService, IPlanetService planetService)
    {
        _airportService = airportService;
        _userManagementMvcService = userManagementMvcService;
        _planetService = planetService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAirports()
    {
        var token = await HttpContext.GetTokenAsync("access_token");

        var result = await _airportService.GetAllAirports(token);

        foreach (Airport airport in result.AirportsList)
        {
            Planet planet = await _planetService.GetPlanetById(airport.IdPlanet, token);
            airport.planetName = planet.Name;            
        }
        return View(result);
    }
}