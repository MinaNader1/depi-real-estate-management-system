namespace depi_real_state_management_system.Models
{
    public class Property
    {
        public int PropertyID { get; set; }
        public string Location { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string OwnerName { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DateAdded { get; set; }

        // Navigation property to represent many-to-one relationship with leases
        public ICollection<Lease> Leases { get; set; }
    }
}
