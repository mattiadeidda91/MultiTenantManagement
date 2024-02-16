using Microsoft.AspNetCore.Authorization;

namespace MultiTenantManagement.Filters
{
    public class AuthRoleAttribute : AuthorizeAttribute
    {
        public AuthRoleAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
