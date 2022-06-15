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
    private readonly IClientService _clientService;

    public HomeController(IHttpContextAccessor accessor, IUserManagementMvcService userManagementMvcService, IClientService clientService)
    {
        _accessor = accessor;
        _userManagementMvcService = userManagementMvcService;
        _clientService = clientService;
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

            // Fetch Client id associated with User and pass it to the Layout view through ViewBag
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            
                Client clientUser = await _clientService.GetClientByEmail(userEmail, await HttpContext.GetTokenAsync("access_token"));
                
            if(clientUser != null)
            {
                HttpContext.Session.SetInt32("ClientUserId", clientUser.IdClient);
               //ViewBag.ClientUserId = clientUser.IdClient;
            }
            else
            {
                HttpContext.Session.SetInt32("ClientUserId", 0);
                // ViewBag.ClientUserId = null;
            }
                   
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