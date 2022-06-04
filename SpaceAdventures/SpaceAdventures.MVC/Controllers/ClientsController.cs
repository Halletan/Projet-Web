using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers;

public class ClientsController : Controller
{
    private readonly IClientService _clientService;
    private readonly IUserManagementMvcService _userManagementMvcService;

    public ClientsController(IClientService clientService, IUserManagementMvcService userManagementMvcService)
    {
        _clientService = clientService;
        _userManagementMvcService = userManagementMvcService;
    }

    [HttpGet]
    public async Task<ActionResult> GetClients()
    {
        return View(await _clientService.GetAllClients(await HttpContext.GetTokenAsync("access_token")));
    }

    [HttpGet]
    public async Task<int> GetClientsCount()
    {
        return await _clientService.GetClientsCount(await HttpContext.GetTokenAsync("access_token"));
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
}