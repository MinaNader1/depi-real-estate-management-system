using depi_real_state_management_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace depi_real_state_management_system.Controllers
{
    public class PropertyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public PropertyController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var properties = _context.Properties.ToList();
            return View(properties);
        }

        // Display property details
        public IActionResult Details(int id)
        {
            var property = _context.Properties
                                   .Include(p => p.Owner)
                                   .FirstOrDefault(p => p.PropertyID == id);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);
        }


        // Display form to create a new property
        [Authorize(Roles ="Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // Add new property to the database
        [HttpPost]
        public async Task<IActionResult> Create(Property property, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Get the currently logged-in user
                var user = await _userManager.GetUserAsync(User);

                // Set the OwnerId to the current user's Id
                property.OwnerId = user.Id;

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    property.ImageUrl = fileName;
                }

                _context.Properties.Add(property);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(property);
        }


    }
}
