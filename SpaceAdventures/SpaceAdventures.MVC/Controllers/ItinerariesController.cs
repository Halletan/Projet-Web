using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers;

[Route("api/mvc/[controller]")]
public class ItinerariesController : Controller
{
    private readonly IItineraryService _itineraryService;
    private readonly IUserManagementMvcService _userManagementMvcService;
    private readonly IAirportService _airportService;
    private readonly IPlanetService _planetService;

    public ItinerariesController(IItineraryService itineraryService, IUserManagementMvcService userManagementMvcService, IAirportService airportService, IPlanetService planetservice)
    {
        _itineraryService = itineraryService;
        _userManagementMvcService = userManagementMvcService;
        _airportService = airportService;
        _planetService = planetservice;
    }

    [HttpGet]
    public async Task<ActionResult> GetItineraries()
    {
        var token = await HttpContext.GetTokenAsync("access_token");

        var result = await _itineraryService.GetAllItineraries(token);

        foreach (Itinerary itinerary in result.ItinerariesList)
        {
            Airport airport1 = await _airportService.GetAirportById(itinerary.IdAirport1, token);
            itinerary.airport1Name = airport1.Name;
            Airport airport2 = await _airportService.GetAirportById(itinerary.IdAirport2, token);
            itinerary.airport2Name = airport2.Name;
            Planet planet1 = await _planetService.GetPlanetById(airport1.IdPlanet, token);
            itinerary.planet1Name = planet1.Name;
            Planet planet2 = await _planetService.GetPlanetById(airport2.IdPlanet, token);
            itinerary.planet2Name = planet2.Name;
        }

        return View(result);
    }

    public async Task<Itinerary> GetItineraryById(int id)
    {
        return await _itineraryService.GetItineraryById(id, await HttpContext.GetTokenAsync("access_token"));
    }

    public async Task<Itineraries> GetItinerariesByDestinationPlanet(string planetName)
    {
       return await _itineraryService.GetItinerariesByDestinationPlanet(planetName, await HttpContext.GetTokenAsync("access_token"));
    }
}