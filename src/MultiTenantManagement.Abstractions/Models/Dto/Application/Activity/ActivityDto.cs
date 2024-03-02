using MultiTenantManagement.Abstractions.Models.Dto.Application.Customer;
using MultiTenantManagement.Abstractions.Models.Dto.Common;
using MultiTenantManagement.Abstractions.Models.Entities.Application;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Activity
{
    public class ActivityDto : BaseDto
    {
        //TODO: Add property Active to Activity.

        public string Name { get; set; } = null!;

        public SiteDto Site { get; set; } = null!;
        public ICollection<CustomerWithoutActivitiesAndSiteDto>? Customers { get; set; }
        public ICollection<RatesDto>? Rates { get; set; }
        public ICollection<HoursActivityDto>? HoursActivities { get; set; }
    }

    public class ActivityWithoutCustomersDto : BaseDto
    {
        public string Name { get; set; } = null!;

        public SiteDto Site { get; set; } = null!;
        public ICollection<RatesDto>? Rates { get; set; }
        public ICollection<HoursActivityDto>? HoursActivities { get; set; }
    }

    public class ActivityWithoutSiteDto : BaseDto
    {
        public string Name { get; set; } = null!;

        public ICollection<CustomerWithoutActivitiesAndSiteDto>? Customers { get; set; }
        public ICollection<RatesDto>? Rates { get; set; }
        public ICollection<HoursActivityDto>? HoursActivities { get; set; }
    }

    public class ActivityWithoutCustomersAndSiteDto : BaseDto
    {
        public string Name { get; set; } = null!;

        public ICollection<RatesDto>? Rates { get; set; }
        public ICollection<HoursActivityDto>? HoursActivities { get; set; }
    }
}
