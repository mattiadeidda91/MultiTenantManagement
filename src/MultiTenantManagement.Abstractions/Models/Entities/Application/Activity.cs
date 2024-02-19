using MultiTenantManagement.Abstractions.Models.Entities.Common;

namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    public class Activity : TenantEntity
    {
        public string? Name { get; set; }
        public Guid SiteId { get; set; }

        public virtual Site Site { get; set; } = null!;
        public virtual ICollection<CustomerActivity>? CustomersActivities { get; set; }
        public virtual ICollection<RatesBase>? RatesBase { get; set; }
        public virtual ICollection<Rates>? Rates { get; set; }
        public virtual ICollection<HoursActivity>? HoursActivities { get; set; }
    }
}
