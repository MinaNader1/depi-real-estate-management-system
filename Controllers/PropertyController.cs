using depi_real_state_management_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace depi_real_state_management_system.Controllers
{
    [Authorize]
    public class PropertyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PropertyController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create(PropertyViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save the uploaded image if it exists
                string uniqueFileName = null;
                if (model.Image != null && model.Image.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }
                }

                // Create a new property instance
                var property = new Property
                {
                    Location = model.Location,
                    Size = model.Size,
                    PricePerNight = model.Price,
                    Description = model.Description,
                    IsAvailable = model.IsAvailable,
                    DateAdded = model.DateAdded,
                    ImageUrl = uniqueFileName, // Save the image path or null
                    OwnerId = model.OwnerId
                };

                _context.Properties.Add(property);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Property updatedProperty)
        {
            if (id != updatedProperty.PropertyID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(updatedProperty);
                await _context.SaveChangesAsync();
                return RedirectToAction("Profile", new { id = updatedProperty.OwnerId });
            }

            return View(updatedProperty);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string propertyId)
        {
            // Convert the propertyId from string to int
            if (int.TryParse(propertyId, out int id))
            {
                var property = await _context.Properties.FindAsync(id);
                if (property != null)
                {
                    _context.Properties.Remove(property);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index"); // Redirect to the appropriate page after deletion
                }
            }

            return NotFound();
        }



    }
}
