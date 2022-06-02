using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpaceAdventures.MVC.Models.NASA;

namespace SpaceAdventures.MVC.Controllers;

public class NASAController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var _httpClient = new HttpClient();
        var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/NASA/GetAlbum/Mars");

        if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<NasaCollection>(content);

        var item = data.collection.items;

        var responseNasa = await _httpClient.GetAsync(item[0].href);
        if (!responseNasa.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

        var contentNasa = await responseNasa.Content.ReadAsStringAsync();
        var dataNasa = JsonConvert.DeserializeObject<List<string>>(contentNasa);
        var video = dataNasa[2];
        ViewBag.video = video;
        return View(data);
    }
}