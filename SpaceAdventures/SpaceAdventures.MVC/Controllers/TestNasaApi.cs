using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Models.NASA;

namespace SpaceAdventures.MVC.Controllers
{
    public class TestNasaApi : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient _httpClient = new HttpClient();
            var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/NASA/GetAlbum/Mars");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve data");
            }

            var content = await response.Content.ReadAsStringAsync();
            NasaCollection data = JsonConvert.DeserializeObject<NasaCollection>(content);

            List<string> UrlImageLst = new List<string>();
            


            return View(data);
        }
    }
}
