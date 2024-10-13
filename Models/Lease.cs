namespace depi_real_state_management_system.Models
{
    public class Lease
    {
        public int LeaseID { get; set; }
        public int PropertyID { get; set; }  // Foreign Key
        public string TenantID { get; set; }  // Foreign Key, replaces 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RentAmount { get; set; }
        public string PaymentFrequency { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public Property Property { get; set; } // Many-to-one relationship
        public ApplicationUser Tenant { get; set; }  // One-to-one relationship with ApplicationUser
    }

}
