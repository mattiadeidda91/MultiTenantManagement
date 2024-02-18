using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Token;

namespace MultiTenantManagement.Abstractions.Models.Dto.Authentication.Login
{
    public class LoginResponseDto
    {
        public TokenDto? TokenDto { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public bool IsSuccess { get; set; }
    }
}
