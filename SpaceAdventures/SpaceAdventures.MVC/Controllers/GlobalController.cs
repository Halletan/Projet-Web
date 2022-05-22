using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers
{
    public class GlobalController : Controller
    {
        private readonly IGlobalService _globalService;

        public GlobalController(IGlobalService globalService)
        {
            _globalService = globalService;
        }

        [HttpGet]
        public async Task<int> GetAircraftsCount()
        {
            return await _globalService.GetAircraftsCount(await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpGet]
        public async Task<int> GetBookingsCount()
        {
            return await _globalService.GetBookingsCount(await HttpContext.GetTokenAsync("access_token"));
        }
    }
}
