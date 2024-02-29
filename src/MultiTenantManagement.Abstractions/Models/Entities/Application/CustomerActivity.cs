using MultiTenantManagement.Abstractions.Models.Entities.Common;

namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    public class CustomerActivity
    {
        public Guid CustomerId { get; set; }
        public Guid ActivityId { get; set; }

        public virtual Activity Activity { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
