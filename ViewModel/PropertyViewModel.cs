using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace depi_real_state_management_system.Models
{
    public class PropertyViewModel
    {
        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Size is required.")]
        public string Size { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Rent amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Rent amount must be a positive number.")]
        public int RentAmount { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile Image { get; set; }

        public string OwnerId { get; set; }
    }
}
