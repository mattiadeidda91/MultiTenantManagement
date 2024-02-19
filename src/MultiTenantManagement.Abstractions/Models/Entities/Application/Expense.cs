using MultiTenantManagement.Abstractions.Models.Entities.Common;

namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    public class Expense : TenantEntity
    {
        public DateTime PaymentDate { get; set; }
        public string? Description { get; set; }
        public string Recipient { get; set; } = null!;
        public double Price { get; set; }


        public string? ActivityName { get; set; }
        public string? SiteName { get; set; }

        //public Guid SiteId { get; set; }
        //public Guid ActivityId { get; set; }

        //public virtual Activity Activity { get; set; } = null!;
        //public virtual Site Site { get; set; } = null!;
    }
}
