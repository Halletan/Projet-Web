using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CodeActions;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Models.NASA;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly INASAService _nasaService;
        private readonly IClientService _clientService;
        private readonly IUserManagementMvcService _userManagement;
        private readonly IItineraryService _itineraryService;
        private readonly IAirportService _airportService;
        private readonly IPlanetService _planetService;
        private readonly IFlightService _flightService;

        #region Constructor
        public BookingController(IBookingService bookingService,
            INASAService nasaService,
            IClientService clientService,
            IUserManagementMvcService userManagement,
            IItineraryService itineraryService,
            IAirportService airportService,
            IPlanetService planetService,
            IFlightService flightService)
        {
            _bookingService = bookingService;
            _nasaService = nasaService;
            _clientService = clientService;
            _userManagement = userManagement;
            _itineraryService = itineraryService;
            _airportService = airportService;
            _planetService = planetService;
            _flightService = flightService;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllBookings()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var result = await _bookingService.GetAllBookings(token);

            foreach (var booking in result.BookingsList)
            {
                Client client = await _clientService.GetClientById(booking.IdClient, token);
                booking.clientFirstName = client.FirstName;
                booking.clientLastName = client.LastName;
                Flight flight = await _flightService.GetFlightById(booking.IdFlight, token);
                Itinerary itinerary = await _itineraryService.GetItineraryById(flight.IdItinerary, token);
                booking.IdItinerary = itinerary.IdItinerary;
                Airport airport1 = await _airportService.GetAirportById(itinerary.IdAirport1, token);
                booking.airport1Name = airport1.Name;
                Airport airport2 = await _airportService.GetAirportById(itinerary.IdAirport2, token);
                booking.airport2Name = airport2.Name;
                Planet planet1 = await _planetService.GetPlanetById(airport1.IdPlanet, token);
                booking.planet1Name = planet1.Name;
                Planet planet2 = await _planetService.GetPlanetById(airport2.IdPlanet, token);
                booking.planet2Name = planet2.Name;
            }
            return View(result);
        }

        public async Task<IActionResult> GetBookingsByClient(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");


            var result = await _bookingService.GetBookingsByClient(id, token);

            foreach(var booking in result.BookingsList) 
            {
                Client client = await _clientService.GetClientById(booking.IdClient, token);
                booking.clientFirstName = client.FirstName;
                booking.clientLastName = client.LastName;
                Flight flight = await _flightService.GetFlightById(booking.IdFlight, token);
                Itinerary itinerary = await _itineraryService.GetItineraryById(flight.IdItinerary, token);
                booking.IdItinerary = itinerary.IdItinerary;
                Airport airport1 = await _airportService.GetAirportById(itinerary.IdAirport1, token);
                booking.airport1Name = airport1.Name;
                Airport airport2 = await _airportService.GetAirportById(itinerary.IdAirport2, token);
                booking.airport2Name = airport2.Name;
                Planet planet1 = await _planetService.GetPlanetById(airport1.IdPlanet, token);
                booking.planet1Name = planet1.Name;
                Planet planet2 = await _planetService.GetPlanetById(airport2.IdPlanet, token);
                booking.planet2Name = planet2.Name;
            }

            return View(result);
        }

        #region Create Booking

        [HttpGet]
        public async Task<IActionResult> CreateBooking(string planetName)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var NasaData = await _nasaService.GetNasaData(planetName,token);

            if (NasaData is null)
            {
                TempData["Message"] = "Info: this trip is currently unavailable, we are sorry for this inconvenience";
                return RedirectToAction("Index");
            }

            var video = await _nasaService.GetNasaVideo(NasaData, token);

            if (video is null)
            {
                TempData["Message"] = "Info: this trip is currently unavailable, we are sorry for this inconvenience";
                return RedirectToAction("Index");
            }

            ViewBag.video = video;

            List<SelectListItem> itinerariesSelectList = new List<SelectListItem>(); 
            itinerariesSelectList.Add(new SelectListItem("itinerary", "Select your itinerary"));
            var itinerariesLst = await _itineraryService.GetItinerariesByDestinationPlanet(planetName, token);
            foreach(var itinerary in itinerariesLst.ItinerariesList)
            {
                var airport1 = await _airportService.GetAirportById(itinerary.IdAirport1, token);
                string airportName1 = airport1.Name;
                var airport2 = await _airportService.GetAirportById(itinerary.IdAirport2, token);
                string airportName2 = airport2.Name;

                var planet1 = await _planetService.GetPlanetById(airport1.IdPlanet, token);
                string planetName1 = planet1.Name;

                string planetName2 = planetName;

                itinerariesSelectList.Add(new SelectListItem(itinerary.IdItinerary.ToString(), "Itinerary : " + planetName1 + " (" + airportName1 + ") ===> " + planetName2 + " (" + airportName2 + ")"));
            }

            ViewBag.itinerariesSelectList = itinerariesSelectList;

            return View();
        }

        [HttpPost, ActionName("CreateBooking")]
        public async Task<IActionResult> PostBooking(Booking booking)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            Client client = new Client()
            {
                LastName = booking.Lastname,
                FirstName = booking.FirstName,
                Email = User.FindFirstValue(ClaimTypes.Email)
            };

            var flight = await _flightService.GetFlightById(booking.IdFlight, token);

            if (booking.NbSeats > flight.RemainingSeats)
            {
                var itinerary = await _itineraryService.GetItineraryById(flight.IdItinerary, token);
                var airport = await _airportService.GetAirportById(itinerary.IdAirport2, token);
                var planet = await _planetService.GetPlanetById(airport.IdPlanet, token);
                var planetName = planet.Name;

                TempData["Message"] = "Warning : The requested number of seats exceeds the number of remaining seats";
                return RedirectToAction( "CreateBooking", "Booking", new {planetName = planetName});
            }

            var ClientExist = await _clientService.ClientExist(client, token);

            if (!ClientExist)
            {
                var createdClient = await _clientService.CreateClient(client, token);
            }   

            var email = User.FindFirstValue(ClaimTypes.Email);
            var clientTemp  = await _clientService.GetClientByEmail(email, token);

            booking.IdClient = clientTemp.IdClient;

            var bookingToShow = await _bookingService.CreateBooking(booking, token);

            if (bookingToShow == null)
            {
                TempData["Message"] = "Error : Unable to confirm this reservation !";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "Success : Your booking has been created successfully";
            return RedirectToAction("Index");
        }


        #endregion

    }
}
