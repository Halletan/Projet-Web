using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpaceAdventures.MVC.Models;
using System.Net.Http.Headers;

namespace SpaceAdventures.MVC.Controllers
{
    [Route("api/mvc/[controller]")]
    public class AircraftsController : Controller
    {
        private readonly HttpClient _httpClient;

        public AircraftsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<ActionResult> GetAircrafts()
        {
            if (User.Identity is not { IsAuthenticated: true })
            {
                TempData["Message"] = "Warning : Please make sure to be authenticated !";
                return RedirectToAction("Index", "Home");
            }

            string? accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Aircrafts");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve data");
            }

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Aircrafts>(content);
            return View(data);
        }
    }
}
