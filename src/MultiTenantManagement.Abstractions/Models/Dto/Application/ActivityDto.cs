using MultiTenantManagement.Abstractions.Models.Dto.Common;
using MultiTenantManagement.Abstractions.Models.Entities.Application;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application
{
    public class ActivityDto : BaseDto
    {
        public string Name { get; set; } = null!;
        public Site Site { get; set; } = null!;
        public ICollection<CustomerDto>? Customers { get; set; }
        public ICollection<RatesBaseDto>? RatesBase { get; set; }
        public ICollection<RatesDto>? Rates { get; set; }
        public ICollection<HoursActivityDto>? HoursActivities { get; set; }
    }
}
