using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restro.Service;

namespace Restro.Controllers
{
    [Authorize(Roles = "aa")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public RoleController(RoleManager<IdentityRole> roleManager,
                             UserManager<IdentityUser> userManager,
                             IRoleService roleService,
                             IUserService userService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _roleService = roleService;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View("AddRole");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole roleModel = new IdentityRole();
                roleModel.Name = roleViewModel.RoleName;
                // Add to db
                IdentityResult result = await _roleManager.CreateAsync(roleModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("AddUserRole");
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(roleViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> AddUserRole()
        {
            ViewData["Users"] = (await _userService.GetAllUsersAsync()).Select(user => user.UserName);
            ViewData["Roles"] = (await _roleService.GetAllRolesAsync()).Select(role => role.Name);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserRole(UserRoleViewModel userRole)
        {
            if (userRole.UserName != "" || userRole.RoleName != "")
            {
                // username, rolename
                if (ModelState.IsValid)
                {
                    //get usermodel
                    IdentityUser userModel = await _userManager.FindByNameAsync(userRole.UserName);
                    IdentityResult result = await _userManager.AddToRoleAsync(userModel, userRole.RoleName);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
                ModelState.AddModelError(string.Empty, "Please Choose User And Role");
            ViewData["Users"] = (await _userService.GetAllUsersAsync()).Select(user => user.UserName);
            ViewData["Roles"] = (await _roleService.GetAllRolesAsync()).Select(role => role.Name);
            return View(userRole);
        }
    }
}
