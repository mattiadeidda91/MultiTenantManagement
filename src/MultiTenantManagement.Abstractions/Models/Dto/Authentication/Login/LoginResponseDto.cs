using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Token;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Authentication.Login
{
    public class LoginResponseDto : ResponseBaseDto
    {
        public TokenDto? TokenDto { get; set; }
    }
}
