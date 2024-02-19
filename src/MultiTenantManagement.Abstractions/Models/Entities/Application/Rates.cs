using MultiTenantManagement.Abstractions.Models.Entities.Common;

namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    public class Rates : TenantEntity
    {
        public double Price { get; set; } //TBD: it was string Rates, mayby it's Name
        public int? DayOfWeek { get; set; } //TBD
        //public Guid CustomerId { get; set; }
        public Guid ActivityId { get; set; }
        //public Guid SiteId { get; set; }

        //public virtual Activity Activity { get; set; } = null!;
    }
}
