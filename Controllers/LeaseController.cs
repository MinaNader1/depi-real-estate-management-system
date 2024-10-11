using Microsoft.AspNetCore.Mvc;
using depi_real_state_management_system.Models;
using System.Linq;

namespace depi_real_state_management_system.Controllers
{
    public class LeaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // List all leases
        public IActionResult Index()
        {
            var leases = _context.Leases.ToList();
            return View(leases);
        }

        // GET: Lease/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lease/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Lease lease)
        {
            if (ModelState.IsValid)
            {
                _context.Leases.Add(lease);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(lease);
        }

        // GET: Lease/Edit/5
        public IActionResult Edit(int id)
        {
            var lease = _context.Leases.Find(id);
            if (lease == null)
            {
                return NotFound();
            }
            return View(lease);
        }

        // POST: Lease/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Lease lease)
        {
            if (id != lease.LeaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Leases.Update(lease);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(lease);
        }

        // GET: Lease/Terminate/5
        public IActionResult Terminate(int id)
        {
            var lease = _context.Leases.Find(id);
            if (lease == null)
            {
                return NotFound();
            }
            return View(lease);
        }

        // POST: Lease/Terminate/5
        [HttpPost, ActionName("Terminate")]
        [ValidateAntiForgeryToken]
        public IActionResult TerminateConfirmed(int id)
        {
            var lease = _context.Leases.Find(id);
            if (lease != null)
            {
                _context.Leases.Remove(lease);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
