using MultiTenantManagement.Abstractions.Models.Dto.Application.Certificate;
using MultiTenantManagement.Abstractions.Models.Dto.Application.FederalCard;
using MultiTenantManagement.Abstractions.Models.Dto.Application.MembershipCard;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Customer.Request
{
    public class RequestCustomer
    {
        public Guid Id { get; set; }
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

        public IEnumerable<CertificateWithoutCustomerDto>? Certificates { get; set; }
        public IEnumerable<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
        public IEnumerable<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }
}
