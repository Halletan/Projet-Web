using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers
{
    public class FlightsController : Controller 
    {

        private readonly IFlightService _flightService;
        

        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
           
        }

        //[HttpGet]

        //public async async <IActionResult> GetFlightsByItinerary(int itineraryId)
        //{
        //    return await _flightService.
        //}

      //  GetFlightsByItinerary
    }
}
