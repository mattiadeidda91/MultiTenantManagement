using MultiTenantManagement.Abstractions.Models.Entities.Common;

namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    public class Site : TenantEntity
    {
        public string? Name { get; set; }

        //public virtual ICollection<Customer>? Customers { get; set; }
        //public virtual ICollection<Activity>? Activities { get; set; }
    }
}
