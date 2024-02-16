using System.Security.Claims;

namespace MultiTenantManagement.Abstractions.Services
{
    public interface IUserService
    {
        string GetUserName();

        Guid GetTenantId();

        public ClaimsIdentity GetIdentity();
    }
}
