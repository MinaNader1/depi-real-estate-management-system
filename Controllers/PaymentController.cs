using depi_real_state_management_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace depi_real_state_management_system.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var payments = await _context.Payments
                .Include(p => p.Lease)
                .Include(p => p.Tenant)
                .ToListAsync();
            return View(payments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Lease)
                .Include(p => p.Tenant)
                .FirstOrDefaultAsync(p => p.PaymentID == id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int leaseId)
        {
            var lease = await _context.Leases.FindAsync(leaseId);
            if (lease == null)
            {
                return NotFound();
            }

            var payment = new Payment
            {
                LeaseID = leaseId,
                TenantID = _userManager.GetUserId(User) // Set TenantID to the logged-in user's ID
            };

            return View(payment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }
    }
}
