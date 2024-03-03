using MultiTenantManagement.Abstractions.Models.Entities.Common;

namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    public class Hours : TenantEntity
    {
        public string? Day { get; set; }
        public string? Hour { get; set; }
        public Guid ActivityId { get; set; }

        public virtual Activity Activity { get; set; } = null!;
    }
}
