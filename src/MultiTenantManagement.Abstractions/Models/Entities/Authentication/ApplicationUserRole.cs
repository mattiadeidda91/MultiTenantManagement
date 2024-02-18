using Microsoft.AspNetCore.Identity;

namespace MultiTenantManagement.Abstractions.Models.Entities.Authentication
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual ApplicationUser? User { get; set; }
        public virtual ApplicationRole? Role { get; set; }
    }
}
