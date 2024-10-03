namespace depi_real_state_management_system.Models
{
    public class Tenant
    {
        public int TenantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Navigation property to represent one-to-many relationship with leases
        public ICollection<Lease> Leases { get; set; }

        // Navigation property to represent one-to-many relationship with payments
        public ICollection<Payment> Payments { get; set; }
    }
}
