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

            TempData["Role"] = await GetRole();
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
        TempData["Role"] = await GetRole();
        return View();
    }

    public async Task<IActionResult> SolarSystem()
    {
        return View();
    }


    private async Task<string> GetRole()
    {
        var idUser = _accessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
        var roles = await _userManagementMvcService.GetUserRole(idUser, await HttpContext.GetTokenAsync("access_token"));
        return await Task.FromResult(roles[0].Name);
    }
}