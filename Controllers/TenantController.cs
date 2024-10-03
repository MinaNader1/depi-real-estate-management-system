using Microsoft.AspNetCore.Mvc;

namespace depi_real_state_management_system.Controllers
{
    public class TenantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
