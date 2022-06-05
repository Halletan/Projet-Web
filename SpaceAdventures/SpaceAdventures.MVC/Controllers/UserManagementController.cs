using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        #region Get List Users

        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userManagementMvcService.GetAllUsers(await HttpContext.GetTokenAsync("access_token"));
            var lst = new List<UserVm>();

            foreach (User user in result.UsersList)
            {
                var vm = new UserVm
                {
                    Email = user.Email,
                    Username = user.Username
                };
                var userRole = await _userManagementMvcService.GetRoleByIdRole(user.idRole,
                    await HttpContext.GetTokenAsync("access_token"));
                vm.Role = userRole.Name;
                lst.Add(vm);
            }
            TempData["Role"] = await _userManagementMvcService.GetRole(await HttpContext.GetTokenAsync("access_token"));
            return View(lst);
        }



        #endregion

        #region CreateUser

        public async Task<IActionResult> CreateUser()
        {
            var rolesList = await _userManagementMvcService.GetAllRole(await HttpContext.GetTokenAsync("access_token"));
            var rolesDropDownList = new SelectList(rolesList.RolesList, "IdRole", "Name");
            ViewBag.listRole = rolesDropDownList;
            return View(new UserInput());
        }

        public async Task<IActionResult> UserSignUp()
        {
            return View();
        }

        [HttpPost]
        [ActionName(nameof(UserSignUp))]
        public async Task<IActionResult> PostUserSignUp(UserInput userInput)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            if (ModelState.IsValid)
            {
                _ = await _userManagementMvcService.CreateUser(accessToken, userInput);
                TempData["Message"] = "Success : Account has been successfully created";
                return RedirectToAction("Login", "Account"); 
            }
           
            return View(userInput);
        }

        [HttpPost]
        [ActionName(nameof(CreateUser))]
        public async Task<IActionResult> PostUser(UserInput userInput)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            if (ModelState.IsValid)
            {
                _ = await _userManagementMvcService.CreateUser(accessToken, userInput);
                TempData["Message"] = "Success : User has been created successfully";
                return RedirectToAction("GetAllUsers");
            }
        
            return View(userInput);
        }

        #endregion

        #region DeleteUser

        public async Task<IActionResult> DeleteUser(string? email)
        {
            var user = await _userManagementMvcService.GetUserByEmail(email,
                await HttpContext.GetTokenAsync("access_token"));
            var role = await _userManagementMvcService.GetRoleByIdRole(user.IdRole, await HttpContext.GetTokenAsync("access_token"));
            user.RoleName=role.Name;
            TempData["Role"] = await _userManagementMvcService.GetRole(await HttpContext.GetTokenAsync("access_token"));
            return View(user);
        }

        [HttpPost]
        [ActionName(nameof(DeleteUser))]
        public async Task<IActionResult> DelUser(int id)
        {
             await _userManagementMvcService.DeleteUser(await HttpContext.GetTokenAsync("access_token"), id);

            //ErrorMessage if Delete NOK.
            TempData["Role"] = await _userManagementMvcService.GetRole(await HttpContext.GetTokenAsync("access_token"));
            TempData["Message"] = "Success : User removed successfully";
            return RedirectToAction("GetAllUsers");
        }

        #endregion

        #region UpdateUser

        public async Task<IActionResult> UpdateUser(string? email)
        {
            var user = await _userManagementMvcService.GetUserByEmail(email,
                await HttpContext.GetTokenAsync("access_token"));
            var role = await _userManagementMvcService.GetRoleByIdRole(user.IdRole, await HttpContext.GetTokenAsync("access_token"));
            user.RoleName = role.Name;
            TempData["Role"] = await _userManagementMvcService.GetRole(await HttpContext.GetTokenAsync("access_token"));
            return View(user);

        }

        [HttpPost]
        [ActionName(nameof(UpdateUser))]
        public async Task<IActionResult> UpdtUser(UserInput userInput)
        {
            await _userManagementMvcService.UpdateUser(await HttpContext.GetTokenAsync("access_token"), userInput);

            //ErrorMessage if Delete NOK.

            TempData["Message"] = "Success : User has been successfully Updated";
            TempData["Role"] = await _userManagementMvcService.GetRole(await HttpContext.GetTokenAsync("access_token"));
            return RedirectToAction(nameof(GetAllUsers));
        }

        #endregion

    }
}
