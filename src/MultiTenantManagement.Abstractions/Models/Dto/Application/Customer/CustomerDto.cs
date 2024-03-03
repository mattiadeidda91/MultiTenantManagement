using MultiTenantManagement.Abstractions.Models.Dto.Application.Activity;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Certificate;
using MultiTenantManagement.Abstractions.Models.Dto.Application.FederalCard;
using MultiTenantManagement.Abstractions.Models.Dto.Application.MembershipCard;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Site;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Customer
{
    public class CustomerDto : BaseDto
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

        public SiteDto? Site { get; set; }
        public ICollection<CertificateWithoutCustomerDto>? Certificates { get; set; }
        public ICollection<ActivityWithoutCustomersAndSiteDto>? Activities { get; set; }
        public virtual ICollection<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
        public virtual ICollection<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }

    public class CustomerWithoutActivitiesDto : BaseDto
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

        public SiteDto? Site { get; set; }
        public ICollection<CertificateWithoutCustomerDto>? Certificates { get; set; }
        public virtual ICollection<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
        public virtual ICollection<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }

    public class CustomerWithoutSiteDto : BaseDto
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

        public ICollection<CertificateWithoutCustomerDto>? Certificates { get; set; }
        public ICollection<ActivityWithoutCustomersAndSiteDto>? Activities { get; set; }
        public virtual ICollection<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
        public virtual ICollection<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }

    public class CustomerWithoutActivitiesAndSiteDto : BaseDto
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

        public ICollection<CertificateWithoutCustomerDto>? Certificates { get; set; }
        public virtual ICollection<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
        public virtual ICollection<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }

    public class CustomerWithoutCertificatesDto
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

        public SiteDto? Site { get; set; }
        public ICollection<ActivityWithoutCustomersAndSiteDto>? Activities { get; set; }
        public virtual ICollection<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
        public virtual ICollection<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }

    public class CustomerWithoutFederalCardsDto : BaseDto
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

        public SiteDto? Site { get; set; }
        public ICollection<CertificateWithoutCustomerDto>? Certificates { get; set; }
        public ICollection<ActivityWithoutCustomersAndSiteDto>? Activities { get; set; }
        public virtual ICollection<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }

    public class CustomerWithoutMembershipCardsDto : BaseDto
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

        public SiteDto? Site { get; set; }
        public ICollection<CertificateWithoutCustomerDto>? Certificates { get; set; }
        public ICollection<ActivityWithoutCustomersAndSiteDto>? Activities { get; set; }
        public virtual ICollection<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
    }
}
