using depi_real_state_management_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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

        // GET: Lease/CreateBooking
        [HttpGet]
        public IActionResult CreateBooking()
        {
            var lease = new Lease
            {
                StartDate = DateTime.Now, // Set default start date to current date
                EndDate = DateTime.Now.AddDays(1) // Set default end date to a day later, for example
            };

            return View(lease);
        }


        // POST: Lease/CreateBooking
        [HttpPost]
        public async Task<IActionResult> CreateBooking(Lease lease, int id)
        {
            if (ModelState.IsValid)
            {
                // Get the current logged-in user (tenant)
                var user = await _userManager.GetUserAsync(User);

                // Fetch the property based on the provided property ID (id)
                var property = _context.Properties
                                       .Include(p => p.Owner)
                                       .FirstOrDefault(p => p.PropertyID == id);

                if (property == null)
                {
                    ModelState.AddModelError("", "Property not found.");
                    return View(lease);
                }

                var leases = new Lease
                {
                    TenantID = user.Id,
                    PropertyID = property.PropertyID,
                    StartDate = lease.StartDate,
                    EndDate = lease.EndDate,
                    // Set other lease properties if needed
                };

                _context.Leases.Add(leases);
                await _context.SaveChangesAsync();

                return RedirectToAction("BookingConfirmation", new { id = leases.LeaseID });
            }

            // If model state is not valid, return to the CreateBooking view with errors
            return View(lease);
        }


        // GET: Lease/BookingConfirmation
        public IActionResult BookingConfirmation(int id)
        {
            return View(id);
        }
    }
}
