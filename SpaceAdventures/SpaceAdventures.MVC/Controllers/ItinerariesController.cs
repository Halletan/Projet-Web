using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers
{
    [Route("api/mvc/[controller]")]
    public class ItinerariesController : Controller
    {

        private readonly IItineraryService _itineraryService;

        public ItinerariesController(IItineraryService itineraryService)
        {
            _itineraryService = itineraryService;
        }

        [HttpGet]
        public async Task<ActionResult> GetItineraries()
        {
            return View(await _itineraryService.GetAllItineraries(await HttpContext.GetTokenAsync("access_token")));
        }

    }
}
