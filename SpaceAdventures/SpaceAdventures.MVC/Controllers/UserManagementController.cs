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
            // if success
                // if user does not exist in DB
                    // Create user in Auth0 (Call API Auth0)
                    // Create user in DB (call API)
                    // Give customer Role set by admin
                    // Give customer role in Auth0
                //Else  (modified user that already exist) or send error message ? 
            //Else Access denied


            string accessToken = "test";

            if (ModelState.IsValid)
            {
                return Ok(_userManagementMvcService.CreateUser(accessToken, user));
            }
            return BadRequest();
        }

        
        public IActionResult UpdateUser()
        {
            User user = new();
           // return View(user);
           return Ok();//To delete
        }

        [HttpPut]
        [ActionName(nameof(UpdateUser))]
        public IActionResult PutUser(User user)
        {
            // if success
                // if user exist in DB
                    //Update Role in DB
                    //Update Role in Auth0 (Call API Auth0)
                //Else  (User does no exist
            //Else Access denied


            string accessToken = "test";

            if (ModelState.IsValid)
            {
                return Ok(_userManagementMvcService.CreateUser(accessToken, user));
            }
            return BadRequest();
        }


    }
}
