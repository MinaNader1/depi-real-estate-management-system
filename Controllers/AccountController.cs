using depi_real_state_management_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace depi_real_state_management_system.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
        }
        public IActionResult Register()
        {
            ApplicationUser user = new ApplicationUser();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmRegister(ApplicationUser user)
        {
            //Bug: I can't save it as a Hash password (it should be ...

            //await _UserManager.CreateAsync(user, user.PasswordHash);
            await _UserManager.CreateAsync(user);
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
			ApplicationUser user = new ApplicationUser();
			return View(user);
        }


        //Bug: the Login doesn't work as expected
        [HttpPost]
        public async Task<IActionResult> ConfirmLogin(ApplicationUser user, bool isPressed)
        {
            var result = await _SignInManager.PasswordSignInAsync(user.Email, user.PasswordHash, isPressed, false);

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
    }
}
