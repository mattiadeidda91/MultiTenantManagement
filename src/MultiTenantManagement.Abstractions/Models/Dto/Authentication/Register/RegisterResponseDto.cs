using MultiTenantManagement.Abstractions.Models.Dto.Common;
using MultiTenantManagement.Abstractions.Models.Entities.Authentication;

namespace MultiTenantManagement.Abstractions.Models.Dto.Authentication.Register
{
    public class RegisterResponseDto : ResponseBaseDto
    {
        public ApplicationUser? User { get; set; }
    }
}
