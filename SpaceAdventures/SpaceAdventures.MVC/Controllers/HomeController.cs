using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity is {IsAuthenticated: true})
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var accessTokenExpiresAt = DateTime.Parse(
                await HttpContext.GetTokenAsync("expires_at") ?? string.Empty,
                CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind);

            var idToken = await HttpContext.GetTokenAsync("id_token");

            var isOwner = User.IsInRole("Owner");

            TempData["Message"] = User.Identity.Name;
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult SolarSystem()
    {
        return View();
    }
}