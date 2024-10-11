using depi_real_state_management_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace depi_real_state_management_system.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _RoleManager = roleManager;
            _context = context;
        }
        //-----------------------------------------Authentication---------------------------------------
        public async Task<IActionResult> Register()
        {
            ApplicationUser user = new ApplicationUser();
            ViewBag.Roles = await _RoleManager.Roles
                .Select(role => new SelectListItem
                {
                    Value = role.Id,
                    Text = role.Name
                }).ToListAsync();
            return View(user);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> ConfirmRegister(ApplicationUser user, string ConfirmPassword, string RoleId)
        {
            // Check if the confirmed password matches
            if (ConfirmPassword == user.PasswordHash)
            {
                // Add the user to the database with the specified password
                var addUser = await _UserManager.CreateAsync(user, user.PasswordHash);

                // If the user was successfully created
                if (addUser.Succeeded)
                {
                    // Retrieve the role using RoleId
                    var role = await _RoleManager.FindByIdAsync(RoleId);

                    if (role != null)
                    {
                        // Add the user to the role by its name
                        var result = await _UserManager.AddToRoleAsync(user, role.Name);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Login");
                        }
                    }
                }
            }

            // If something goes wrong, show an error view
            return RedirectToAction("Error");
        }


        public IActionResult Login()
        {
			ApplicationUser user = new ApplicationUser();
			return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmLogin(ApplicationUser user)
        {
            var result = await _SignInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, user.RememberMe, false);

            if (result.Succeeded)
            {
                return Redirect("/home/index");
            }
            ViewBag.Status = 0;
            ViewBag.ErrorMessage = "Username or Password is incorrect";
            return View("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        //-----------------------------------------Authorization---------------------------------------

        public async Task<IActionResult> AddRole(string role)
        {
            IdentityRole roleModel = new IdentityRole()
            {
                Name = role,
            };
            
            await _RoleManager.CreateAsync(roleModel);
            return View();
        }



        public async Task<IActionResult> Profile(string id)
        {
            // Find the user by their ID
            var user = await _UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // Fetch the properties owned by the user
            var userProperties = await _context.Properties
                                               .Where(p => p.OwnerId == id)
                                               .ToListAsync();

            // Pass the user and properties to the view using ViewBag
            ViewBag.User = user;
            ViewBag.Properties = userProperties;

            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
