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
            TempData["Role"] = await _userManagement.GetRole(await HttpContext.GetTokenAsync("access_token"));
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateBooking(string planetName)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var NasaData =await _nasaService.GetNasaData(planetName,token);
            var video = await _nasaService.GetNasaVideo(NasaData, token);
            ViewBag.video = video;
            TempData["Role"] = await _userManagement.GetRole(await HttpContext.GetTokenAsync("access_token"));
            return View();
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
