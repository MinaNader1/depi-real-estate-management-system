using depi_real_state_management_system.Models;
using Microsoft.AspNetCore.Mvc;

namespace depi_real_state_management_system.Controllers
{
    public class PropertyController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PropertyController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var properties = _context.Properties.ToList();
            return View(properties);
        }

        // Display property details
        public IActionResult Details(int id)
        {
            var property = _context.Properties.FirstOrDefault(p => p.PropertyID == id);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);
        }

        // Display form to create a new property
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
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Generate a unique filename
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/images", fileName);

                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    // Save the filename in the property object
                    property.ImageUrl = fileName;
                }

                // Add the property to the database
                _context.Properties.Add(property);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(property);
        }

    }
}
