using Microsoft.AspNetCore.Http;
using MultiTenantManagement.Abstractions.Extensions;
using MultiTenantManagement.Abstractions.Services;
using System.Security.Claims;

namespace MultiTenantManagement.Dependencies.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Guid GetTenantId()
        {
            CheckHttpContextAccessorIsNull();

            var tenantIdClaim = httpContextAccessor!.HttpContext!.User.GetTenantId();

            if (Guid.TryParse(tenantIdClaim, out var tenantId))
            {
                return tenantId;
            }

            return Guid.Empty;
        }

        public string GetUserName() 
        {
            CheckHttpContextAccessorIsNull();

            var name = httpContextAccessor!.HttpContext!.User?.Identity?.Name;

            ArgumentNullException.ThrowIfNull(name);

            return name;
        }

        public ClaimsIdentity GetIdentity()
        {
            CheckHttpContextAccessorIsNull();

            var claimIdentity = httpContextAccessor!.HttpContext!.User.Identity as ClaimsIdentity;

            ArgumentNullException.ThrowIfNull(claimIdentity);

            return claimIdentity;
        }

        private void CheckHttpContextAccessorIsNull()
        {
            ArgumentNullException.ThrowIfNull(httpContextAccessor);
            ArgumentNullException.ThrowIfNull(httpContextAccessor.HttpContext);
        }
    }
}
