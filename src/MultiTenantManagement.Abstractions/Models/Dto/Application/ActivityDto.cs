using MultiTenantManagement.Abstractions.Models.Dto.Common;
using MultiTenantManagement.Abstractions.Models.Entities.Application;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application
{
    public class ActivityDto : BaseDto
    {
        public string Name { get; set; } = null!;

        public Site Site { get; set; } = null!;
        public ICollection<CustomerDto>? Customers { get; set; }
        public ICollection<RatesDto>? Rates { get; set; }
        public ICollection<HoursActivityDto>? HoursActivities { get; set; }
    }

    public class RequestActivity
    {
        public string Name { get; set; } = null!;
        public Guid SiteId { get; set; }
        public Guid CustomerId { get; set; }

        //TODO: Add Rates and Hours
    }
}
