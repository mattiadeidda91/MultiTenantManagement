using Microsoft.AspNetCore.Authorization;

namespace MultiTenantManagement.Attributes
{
    public class AuthRoleAttribute : AuthorizeAttribute
    {
        public AuthRoleAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
