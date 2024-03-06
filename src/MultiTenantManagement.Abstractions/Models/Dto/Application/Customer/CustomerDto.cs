using MultiTenantManagement.Abstractions.Models.Dto.Application.Activity;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Certificate;
using MultiTenantManagement.Abstractions.Models.Dto.Application.FederalCard;
using MultiTenantManagement.Abstractions.Models.Dto.Application.MembershipCard;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Site;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Customer
{
    public interface ISite
    {
        public SiteDto? Site { get; set; }
    }

    public interface ICertificate
    {
        public ICollection<CertificateWithoutCustomerDto>? Certificates { get; set; }
    }

    public interface IActivities
    {
        public ICollection<ActivityWithoutCustomersAndSiteDto>? Activities { get; set; }
    }

    public interface IFederalCards
    {
        public ICollection<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
    }

    public interface IMembershipCards
    {
        public ICollection<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }

    public class CustomerBaseDto : BaseDto
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
        public string? VatNumber { get; set; }
        public string? Company { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? MobilePhone { get; set; }
        public string? Note { get; set; }
    }

    public class CustomerDto : CustomerBaseDto, ISite, ICertificate, IActivities, IFederalCards, IMembershipCards
    {
        public SiteDto? Site { get; set; }
        public ICollection<CertificateWithoutCustomerDto>? Certificates { get; set; }
        public ICollection<ActivityWithoutCustomersAndSiteDto>? Activities { get; set; }
        public ICollection<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
        public ICollection<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }

    public class CustomerWithoutActivitiesDto : CustomerBaseDto, ISite, ICertificate, IFederalCards, IMembershipCards
    {
        public SiteDto? Site { get; set; }
        public ICollection<CertificateWithoutCustomerDto>? Certificates { get; set; }
        public ICollection<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
        public ICollection<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }

    public class CustomerWithoutSiteDto : CustomerBaseDto, ICertificate, IActivities, IFederalCards, IMembershipCards
    {
        public ICollection<CertificateWithoutCustomerDto>? Certificates { get; set; }
        public ICollection<ActivityWithoutCustomersAndSiteDto>? Activities { get; set; }
        public ICollection<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
        public ICollection<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }

    public class CustomerWithoutActivitiesAndSiteDto : CustomerBaseDto, ICertificate, IFederalCards, IMembershipCards
    {
        public ICollection<CertificateWithoutCustomerDto>? Certificates { get; set; }
        public ICollection<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
        public ICollection<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }

    public class CustomerWithoutCertificatesDto : CustomerBaseDto, ISite, IActivities, IFederalCards, IMembershipCards
    {
        public SiteDto? Site { get; set; }
        public ICollection<ActivityWithoutCustomersAndSiteDto>? Activities { get; set; }
        public ICollection<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
        public ICollection<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }

    public class CustomerWithoutFederalCardsDto : CustomerBaseDto, ISite, ICertificate, IActivities, IMembershipCards
    {
        public SiteDto? Site { get; set; }
        public ICollection<CertificateWithoutCustomerDto>? Certificates { get; set; }
        public ICollection<ActivityWithoutCustomersAndSiteDto>? Activities { get; set; }
        public ICollection<MembershipCardWithoutCustomerDto>? MembershipCards { get; set; }
    }

    public class CustomerWithoutMembershipCardsDto : CustomerBaseDto, ISite, ICertificate, IActivities, IFederalCards
    {
        public SiteDto? Site { get; set; }
        public ICollection<CertificateWithoutCustomerDto>? Certificates { get; set; }
        public ICollection<ActivityWithoutCustomersAndSiteDto>? Activities { get; set; }
        public ICollection<FederalCardWithoutCustomerDto>? FederalCards { get; set; }
    }
}
