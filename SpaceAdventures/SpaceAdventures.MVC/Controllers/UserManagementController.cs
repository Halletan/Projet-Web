using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserManagementMvcService _userManagementMvcService;
        private readonly IClientService _clientService;

        public UserManagementController(IUserManagementMvcService userManagementMvcService, IClientService clientService)
        {
            _userManagementMvcService = userManagementMvcService;
            _clientService = clientService;
        }

        #region Get List Users

        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userManagementMvcService.GetAllUsers(await HttpContext.GetTokenAsync("access_token"));
            
            foreach (User user in result.UsersList)
            {
                var userRole = await _userManagementMvcService.GetRoleByIdRole(user.IdRole,
                    await HttpContext.GetTokenAsync("access_token"));
                user.RoleName = userRole.Name;           
            }
            return View(result);
        }
        [HttpGet]
        [Route("Users/UserExists")]
        public async Task<bool> UserExists(string? email)
        {
            var users = await _userManagementMvcService.GetAllUsers(await HttpContext.GetTokenAsync("access_token"));
            return users.UsersList.Any(u => u.Email == email);
        }

        #endregion

        #region CreateUser
        public async Task<IActionResult> CreateUser()
        {
            var rolesList = await _userManagementMvcService.GetAllRole(await HttpContext.GetTokenAsync("access_token"));
            var rolesDropDownList = new SelectList(rolesList.RolesList, "IdRole", "Name");
            ViewBag.listRole = rolesDropDownList;
            return View(new User());
        }

        public async Task<IActionResult> UserSignUp()
        {
            return View();
        }

        [HttpPost]
        [ActionName(nameof(UserSignUp))]
        public async Task<IActionResult> PostUserSignUp(User user)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");


            if (ModelState.IsValid)
            {
                _ = await _userManagementMvcService.CreateUserSignUp(accessToken, user);
                TempData["Message"] = "Success: Account has been successfully created";
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }

        [HttpPost]
        [ActionName(nameof(CreateUser))]
        public async Task<IActionResult> PostUser(User user)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            user.Password = "Default";

            if (ModelState.IsValid)
            {
                _ = await _userManagementMvcService.CreateUser(accessToken, user);
                TempData["Message"] = "Success: User has been created successfully";
                return RedirectToAction("GetAllUsers");
            }

            return View(user);
        }

        #endregion

        #region DeleteUser

        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userManagementMvcService.GetUserById(id, await HttpContext.GetTokenAsync("access_token"));
            //var user = await _userManagementMvcService.GetUserByEmail(email,
            //    await HttpContext.GetTokenAsync("access_token"));
            var role = await _userManagementMvcService.GetRoleByIdRole(user.IdRole, await HttpContext.GetTokenAsync("access_token"));
            user.RoleName = role.Name;
            return View(user);
        }

        [HttpPost]
        [ActionName(nameof(DeleteUser))]
        public async Task<IActionResult> DelUser(int id)
        {
            var client = await _clientService.GeClientByIdUser(id, await HttpContext.GetTokenAsync("access_token"));
            
            if (client != null)
            {
                TempData["Message"] = "Unable to delete this user";
                RedirectToAction("GetAllUsers");
            }

            await _userManagementMvcService.DeleteUser(await HttpContext.GetTokenAsync("access_token"), id);
            TempData["Message"] = "Success: User removed successfully";
            return RedirectToAction("GetAllUsers");
        }

        #endregion

        #region UpdateUser

        public async Task<IActionResult> UpdateUser(string? email)
        {
            var user = await _userManagementMvcService.GetUserByEmail(email, await HttpContext.GetTokenAsync("access_token"));
            var role = await _userManagementMvcService.GetRoleByIdRole(user.IdRole, await HttpContext.GetTokenAsync("access_token"));
            ViewBag.roleName = role.Name;
            
            var rolesList = await _userManagementMvcService.GetAllRole(await HttpContext.GetTokenAsync("access_token"));
            var rolesDropDownList = new SelectList(rolesList.RolesList, "IdRole", "Name");
            ViewBag.listRole = rolesDropDownList;
            return View(user);
        }

        [HttpPost]
        [ActionName(nameof(UpdateUser))]
        public async Task<IActionResult> UpdtUser(User user)
        {

            await _userManagementMvcService.UpdateUser(await HttpContext.GetTokenAsync("access_token"), user);
            
            TempData["Message"] = "Success: User has been successfully Updated";
            return RedirectToAction(nameof(GetAllUsers));
        }

        #endregion

    }
}
