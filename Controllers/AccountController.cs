using Microsoft.AspNetCore.Mvc;

namespace depi_real_state_management_system.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
