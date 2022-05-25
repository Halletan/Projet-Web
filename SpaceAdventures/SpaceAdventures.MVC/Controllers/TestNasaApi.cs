using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpaceAdventures.MVC.Models.NASA;

namespace SpaceAdventures.MVC.Controllers;

public class TestNasaApi : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var _httpClient = new HttpClient();
        var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/NASA/GetAlbum/Mars");

        if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<NasaCollection>(content);

        var UrlImageLst = new List<string>();


        return View(data);
    }
}