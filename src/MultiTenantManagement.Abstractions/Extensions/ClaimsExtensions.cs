using System.Security.Claims;
using System.Security.Principal;

namespace MultiTenantManagement.Abstractions.Extensions
{
    public static class ClaimsExtensions
    {
        public static Guid GetId(this IPrincipal user)
        {
            var value = GetClaimValue(user, ClaimTypes.NameIdentifier);
            return Guid.Parse(value);
        }

        public static string GetFirstName(this IPrincipal user)
            => GetClaimValue(user, ClaimTypes.GivenName);

        public static string GetLastName(this IPrincipal user)
            => GetClaimValue(user, ClaimTypes.Surname);

        public static string GetEmail(this IPrincipal user)
            => GetClaimValue(user, ClaimTypes.Email);

        public static string GetTenantId(this IPrincipal user)
            => GetClaimValue(user, ClaimTypes.GroupSid);

        public static IEnumerable<string> GetRoles(this IPrincipal user)
        {
            var values = ((ClaimsPrincipal)user).FindAll(ClaimTypes.Role).Select(c => c.Value);
            return values;
        }

        public static string GetClaimValue(this IPrincipal user, string claimType)
        {
            var value = ((ClaimsPrincipal)user).FindFirst(claimType)?.Value;
            return value!;
        }
    }
}
