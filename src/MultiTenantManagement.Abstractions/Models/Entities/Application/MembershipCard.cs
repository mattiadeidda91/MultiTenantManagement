using MultiTenantManagement.Abstractions.Models.Entities.Common;

namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    public class MembershipCard : TenantEntity
    {
        public string Card { get; set; } = null!;
        public DateTime MembershipDate { get; set; }
        public DateTime CardExpireDate { get; set; }
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
