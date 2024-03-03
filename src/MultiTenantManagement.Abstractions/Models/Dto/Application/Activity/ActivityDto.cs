using MultiTenantManagement.Abstractions.Models.Dto.Application.Customer;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Hours;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Rates;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Site;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Activity
{
    public class ActivityDto : BaseDto
    {
        //TODO: Add property Active to Activity.

        public string Name { get; set; } = null!;

        public SiteDto Site { get; set; } = null!;
        public ICollection<CustomerWithoutActivitiesAndSiteDto>? Customers { get; set; }
        public ICollection<RatesWithoutActivityDto>? Rates { get; set; }
        public ICollection<HoursWithoutActivityDto>? Hours { get; set; }
    }

    public class ActivityWithoutCustomersDto : BaseDto
    {
        public string Name { get; set; } = null!;

        public SiteDto Site { get; set; } = null!;
        public ICollection<RatesWithoutActivityDto>? Rates { get; set; }
        public ICollection<HoursWithoutActivityDto>? Hours { get; set; }
    }

    public class ActivityWithoutSiteDto : BaseDto
    {
        public string Name { get; set; } = null!;

        public ICollection<CustomerWithoutActivitiesAndSiteDto>? Customers { get; set; }
        public ICollection<RatesWithoutActivityDto>? Rates { get; set; }
        public ICollection<HoursWithoutActivityDto>? Hours { get; set; }
    }

    public class ActivityWithoutCustomersAndSiteDto : BaseDto
    {
        public string Name { get; set; } = null!;

        public ICollection<RatesWithoutActivityDto>? Rates { get; set; }
        public ICollection<HoursWithoutActivityDto>? Hours { get; set; }
    }

    public class ActivityWithoutRatesDto : BaseDto
    {
        //TODO: Add property Active to Activity.

        public string Name { get; set; } = null!;

        public SiteDto Site { get; set; } = null!;
        public ICollection<CustomerWithoutActivitiesAndSiteDto>? Customers { get; set; }
        public ICollection<HoursWithoutActivityDto>? Hours { get; set; }
    }

    public class ActivityWithoutHoursDto : BaseDto
    {
        //TODO: Add property Active to Activity.

        public string Name { get; set; } = null!;

        public SiteDto Site { get; set; } = null!;
        public ICollection<CustomerWithoutActivitiesAndSiteDto>? Customers { get; set; }
        public ICollection<RatesWithoutActivityDto>? Rates { get; set; }
    }
}
