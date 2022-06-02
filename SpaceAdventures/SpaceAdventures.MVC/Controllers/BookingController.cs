using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
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

        public BookingController(IBookingService bookingService,INASAService nasaService,IClientService clientService)
        {
            _bookingService = bookingService ;
            _nasaService = nasaService ;
            _clientService = clientService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateBooking(string planetName)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var NasaData =await _nasaService.GetNasaData(planetName,token);
            var video=await _nasaService.GetNasaVideo(NasaData, token);
            ViewBag.video = video;
            return View();
        }

        [HttpPost,ActionName("CreateBooking")]
        public async Task<IActionResult> PostBooking(BookingVm booking)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            Client client = new Client()
            {
                LastName = booking.Lastname,
                FirstName = booking.FirstName,
                Email = booking.Email,
               
            };

             bool created = await _clientService.CreateClient(client, token);

            return View();
        }

    }
}
