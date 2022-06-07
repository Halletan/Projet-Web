using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpaceAdventures.MVC.Models;
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
        [HttpGet]
        public async Task<List<SelectListItem>> GetFlightsByItinerary(int idItinerary)
        {
            var flightlist = await _flightService.GetFlightsByItinerary(idItinerary, await HttpContext.GetTokenAsync("access_token"));

            List<SelectListItem> lstFlight = new List<SelectListItem>();

            foreach (var f in flightlist.FlightsList)
            {
                SelectListItem item = new SelectListItem()
                {
                    Value = f.IdFlight.ToString(),
                    Text = "Departure time : " + f.DepartureTime + " -- Arrival Time : " + f.ArrivalTime + " (Remaining seats : " + f.RemainingSeats + ") -- Price : " + f.Price
                };
                lstFlight.Add(item);
            }
            return lstFlight;

        }
        public async Task<Flight> GetFlightById(int IdFlight)
        {
            return await _flightService.GetFlightById(IdFlight, await HttpContext.GetTokenAsync("access_token"));
        }
        public async Task<double> GetPriceByFlight(int IdFlight)
        {
            var flight= await _flightService.GetFlightById(IdFlight, await HttpContext.GetTokenAsync("access_token"));
            return flight.Price;
        }

    }
}
