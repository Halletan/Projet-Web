using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers
{
    [Route("api/mvc/[controller]")]
    public class AircraftsController : Controller
    {
        private readonly IAircraftService _aircraftService;

        public AircraftsController(IAircraftService aircraftService)
        {
            _aircraftService = aircraftService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAircrafts()
        {
            return View(await _aircraftService.GetAllAircrafts(await HttpContext.GetTokenAsync("access_token")));
        }
    }
}
