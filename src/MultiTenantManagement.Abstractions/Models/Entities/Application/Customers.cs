using MultiTenantManagement.Abstractions.Models.Entities.Common;

namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    public class Customer : TenantEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public string? BirthProvince { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Region { get; set; }
        public string? Province { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? FiscalCode { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? MobilePhone { get; set; }
        public string? Note { get; set; }
        public Guid SiteId { get; set; }

        public virtual Site? Site { get; set; }
        public virtual ICollection<Certificate>? Certificates { get; set; }
        public virtual ICollection<CustomerActivity> CustomersActivities { get; set; } = null!;
        public virtual ICollection<FederalCard>? FederalCards { get; set; }
        public virtual ICollection<MembershipCard>? MembershipCards { get; set; }
    }
}
