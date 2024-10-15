using depi_real_state_management_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace depi_real_state_management_system.Controllers
{
    public class LeaseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LeaseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var leases = await _context.Leases
                .Include(l => l.Property) // Include related property details
                .Include(l => l.Tenant)   // Include related tenant details
                .ToListAsync();

            return View(leases);
        }

        public async Task<IActionResult> Details(int id)
        {
            var lease = await _context.Leases
                .Include(l => l.Property) // Include related property
                .Include(l => l.Tenant)   // Include related tenant
                .FirstOrDefaultAsync(m => m.LeaseID == id);

            if (lease == null)
            {
                return NotFound();
            }

            return View(lease);
        }

        // GET: Lease/CreateBook
        [HttpGet]
        public async Task<IActionResult> CreateBooking(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            var lease = new Lease
            {
                PropertyID = id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1), // Default to 1-night booking
            };

         
            return View(lease); // CreateBooking view
        }

        // POST: Lease/CreateBooking
        [HttpPost]
        public async Task<IActionResult> CreateBooking(Lease lease)
        {
            if (ModelState.IsValid)
            {
                _context.Leases.Add(lease);  // Add the lease to the context
                await _context.SaveChangesAsync();  // Save changes to the database

                return RedirectToAction(nameof(Index));  // Redirect to a confirmation page or list of properties
            }

            lease.Property = await _context.Properties.FindAsync(lease.PropertyID);
            lease.Tenant = await _context.Users.FindAsync(lease.TenantID);

            // If ModelState is not valid, return the form with validation errors
            return View(lease);
        }



        // Optional: Logic to terminate the lease before 2 days
        [HttpPost]
        public async Task<IActionResult> TerminateLease(int leaseId)
        {
            var lease = await _context.Leases.FindAsync(leaseId);
            if (lease == null)
            {
                return NotFound();
            }

            // Check if the termination is allowed (two days before the start date)
            if (DateTime.Now < lease.StartDate.AddDays(-2))
            {
                lease.Status = "Terminated";
                _context.Update(lease);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to lease listing page
            }

            // If not allowed, show an error message
            ModelState.AddModelError("", "Lease cannot be terminated less than 2 days before the start date.");
            return View("Details", lease);
        }
    }
}
