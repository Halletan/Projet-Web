using System.Globalization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers;

public class AccountController : Controller
{

    private readonly IUserManagementMvcService _userManagementMvcService;

    public AccountController(IUserManagementMvcService userManagementMvcService)
    {
        _userManagementMvcService = userManagementMvcService;
    }

    // Login : To add the Login action, we call ChallengeAsync and pass "Auth0" as the authentication scheme.
    // This will invoke the OIDC authentication handler that we've registered in the ConfigureServices method.
    public async Task Login(string returnUrl = "/")
    {
        await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties {RedirectUri = returnUrl});
    }

    // Logout : For the Logout action, you need to sign the user out of both middlewares.

    [Authorize]
    public async Task Logout()
    {
        await HttpContext.SignOutAsync("Auth0",
            new AuthenticationProperties {RedirectUri = Url.Action("Index", "Home")});
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}