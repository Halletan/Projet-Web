using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using NuGet.Versioning;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers;

public class HomeController : Controller
{

    private readonly IHttpContextAccessor _accessor;
    private readonly IUserManagementMvcService _userManagementMvcService;

    public HomeController(IHttpContextAccessor accessor, IUserManagementMvcService userManagementMvcService)
    {
        _accessor = accessor;
        _userManagementMvcService = userManagementMvcService;
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

            HttpContext.Session.SetString("Role", await _userManagementMvcService.GetRole(await HttpContext.GetTokenAsync("access_token")));


           TempData["Role"] = await _userManagementMvcService.GetRole(await HttpContext.GetTokenAsync("access_token"));
            TempData["Message"] = "Logged as : " + User.Identity.Name;
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    public async Task<IActionResult> About()
    {
        return View();  
    }

    public async Task<IActionResult> Dashboard()
    {
        return View();
    }

    public async Task<IActionResult> SolarSystem()
    {
        return View();
    }
}