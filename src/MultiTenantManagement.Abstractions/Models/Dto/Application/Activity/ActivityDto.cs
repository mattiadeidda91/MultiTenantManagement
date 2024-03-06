using MultiTenantManagement.Abstractions.Models.Dto.Application.Customer;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Hours;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Rates;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Site;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Activity
{
    public class ActivityBaseDto : BaseDto
    {
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
    }

    public class ActivityDto : ActivityBaseDto
    {
        public SiteDto Site { get; set; } = null!;
        public ICollection<CustomerWithoutActivitiesAndSiteDto>? Customers { get; set; }
        public ICollection<RatesWithoutActivityDto>? Rates { get; set; }
        public ICollection<HoursWithoutActivityDto>? Hours { get; set; }
    }

    public class ActivityWithoutCustomersDto : ActivityBaseDto
    {
        public SiteDto Site { get; set; } = null!;
        public ICollection<RatesWithoutActivityDto>? Rates { get; set; }
        public ICollection<HoursWithoutActivityDto>? Hours { get; set; }
    }

    public class ActivityWithoutSiteDto : ActivityBaseDto
    {
        public ICollection<CustomerWithoutActivitiesAndSiteDto>? Customers { get; set; }
        public ICollection<RatesWithoutActivityDto>? Rates { get; set; }
        public ICollection<HoursWithoutActivityDto>? Hours { get; set; }
    }

    public class ActivityWithoutCustomersAndSiteDto : ActivityBaseDto
    {
        public ICollection<RatesWithoutActivityDto>? Rates { get; set; }
        public ICollection<HoursWithoutActivityDto>? Hours { get; set; }
    }

    public class ActivityWithoutRatesDto : ActivityBaseDto
    {
        public SiteDto Site { get; set; } = null!;
        public ICollection<CustomerWithoutActivitiesAndSiteDto>? Customers { get; set; }
        public ICollection<HoursWithoutActivityDto>? Hours { get; set; }
    }

    public class ActivityWithoutHoursDto : ActivityBaseDto
    {
        public SiteDto Site { get; set; } = null!;
        public ICollection<CustomerWithoutActivitiesAndSiteDto>? Customers { get; set; }
        public ICollection<RatesWithoutActivityDto>? Rates { get; set; }
    }
}
