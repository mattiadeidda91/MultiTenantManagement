using MultiTenantManagement.Abstractions.Models.Dto.Application.Hours;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Rates;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Activity.Request
{
    public class RequestActivity
    {
        public string Name { get; set; } = null!;
        public Guid SiteId { get; set; }
        public Guid CustomerId { get; set; }

        public IEnumerable<RatesWithoutActivityDto>? Rates { get; set; }
        public IEnumerable<HoursDto>? Hours { get; set; }
    }
}
