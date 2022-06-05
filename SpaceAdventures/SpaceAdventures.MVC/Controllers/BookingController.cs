using System.Security.Claims;
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
        private readonly IUserManagementMvcService _userManagement;

        public BookingController(IBookingService bookingService,INASAService nasaService, IClientService clientService, IUserManagementMvcService userManagement)
        {
            _bookingService = bookingService;
            _nasaService = nasaService;
            _clientService = clientService;
            _userManagement = userManagement;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateBooking(string planetName)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var nasaData =await _nasaService.GetNasaData(planetName,token);

            if (nasaData is not null)
            {
                var video = await _nasaService.GetNasaVideo(nasaData, token);
                ViewBag.video = video;
                return View();
            }

            TempData["Message"] = "Info: this trip is currently unavailable, we are sorry for this inconvenience";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("CreateBooking")]
        public async Task<IActionResult> PostBooking(BookingVm booking)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            Client client = new Client()
            {
                LastName = booking.Lastname,
                FirstName = booking.FirstName,
                Email = User.FindFirstValue(ClaimTypes.Email)
            };

            bool ClientExist = await _clientService.ClientExist(client, token);
            if (!ClientExist)
            {
                bool created = await _clientService.CreateClient(client, token);
            }
            TempData["Role"] = await _userManagement.GetRole(await HttpContext.GetTokenAsync("access_token"));
            return View();
        }

    }
}
