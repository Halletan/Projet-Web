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
            Users result = await _userManagementMvcService.GetAllUsers(await HttpContext.GetTokenAsync("access_token"));
            List<UserVm> lst = new List<UserVm>();

            foreach( User user in result.UsersList)
            {
                UserVm vm = new UserVm();
                vm.Email=user.Email;
                vm.Username=user.Username;
                UserRole userRole = await _userManagementMvcService.GetRoleByIdRole(user.idRole, await HttpContext.GetTokenAsync("access_token"));
                vm.Role = userRole.name;
                lst.Add(vm);
            }
            

            return View(lst);
        }
        #endregion

        #region CreateUser
        public IActionResult CreateUser()
        {
            IEnumerable<SelectListItem> listRole = new List<SelectListItem>()
            {
                new SelectListItem(){Value="1", Text="Admin"},
                new SelectListItem(){Value="2", Text="Admina"},
                new SelectListItem(){Value="3", Text="Owner"},
                new SelectListItem(){Value="4", Text="User"}
            };
            ViewBag.listRole = listRole;


            UserInput user = new();
            return View(user);
        }

        [HttpPost]
        [ActionName(nameof(CreateUser))]
        public async Task<IActionResult> PostUser(UserInput userInput)
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");

            if (ModelState.IsValid)
            {               
                User userCreated=  await _userManagementMvcService.CreateUser(accessToken, userInput);

                return View(userCreated); // Sera une RedirectToAction vers GetUserDetails(user)
            }
            return View(userInput);
        }
        #endregion

        #region UpdateUser
        //public IActionResult UpdateUser()
        //{
        //    User user = new();
        //   // return View(user);
        //   return Ok();//To delete
        //}

        //[HttpPut]
        //[ActionName(nameof(UpdateUser))]
        //public IActionResult PutUser(User user)
        //{
        //    // if success
        //        // if user exist in DB
        //            //Update Role in DB
        //            //Update Role in Auth0 (Call API Auth0)
        //        //Else  (User does no exist
        //    //Else Access denied


        //    string accessToken = "test";

        //    if (ModelState.IsValid)
        //    {
        //        return Ok(_userManagementMvcService.CreateUser( user));
        //    }
        //    return BadRequest();
        //}
        #endregion

    }
}
