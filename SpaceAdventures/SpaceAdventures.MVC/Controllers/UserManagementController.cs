using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers
{
    public class UserManagementController : Controller
    {

        private readonly IUserManagementMvcService _userManagementMvcService;

        public UserManagementController(IUserManagementMvcService userManagementMvcService)
        {
            _userManagementMvcService = userManagementMvcService;
        }


        public IActionResult CreateUser()
        {
            User user = new();
            return View(user);
        }

        [HttpPost]
        [ActionName(nameof(CreateUser))]
        public IActionResult PostUser(User user)
        {
            

            string accessToken = "test";

            if (ModelState.IsValid)
            {
                return Ok(_userManagementMvcService.CreateUser(accessToken, user));
            }
            return BadRequest();
        }


    }
}
