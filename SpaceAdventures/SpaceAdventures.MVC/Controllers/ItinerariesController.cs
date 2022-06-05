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

    public ItinerariesController(IItineraryService itineraryService, IUserManagementMvcService userManagementMvcService)
    {
        _itineraryService = itineraryService;
        _userManagementMvcService = userManagementMvcService;
    }

    [HttpGet]
    public async Task<ActionResult> GetItineraries()
    {
        TempData["Role"] = await _userManagementMvcService.GetRole(await HttpContext.GetTokenAsync("access_token"));
        return View(await _itineraryService.GetAllItineraries(await HttpContext.GetTokenAsync("access_token")));
    }



    [HttpGet]
    public async Task<Itineraries> GetItinerariesByDestinationPlanet(string planetName)
    {
       return await _itineraryService.GetItinerariesByDestinationPlanet(planetName, await HttpContext.GetTokenAsync("access_token"));



    }
}