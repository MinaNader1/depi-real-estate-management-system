using System.ComponentModel.DataAnnotations;

namespace depi_real_state_management_system.Models
{
    public class Property
    {
        public int PropertyID { get; set; } // This should be auto-incremented by the database

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Size is required.")]
        public string Size { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Owner Name is required.")]
        public string OwnerName { get; set; }

        public bool IsAvailable { get; set; } = true; // Default to true if not specified

        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; } = DateTime.Now; // Set default value

        [Url(ErrorMessage = "Invalid Image URL.")]
        public string ImageUrl { get; set; }

        // Navigation property for Leases
        public ICollection<Lease> Leases { get; set; }
    }
}
