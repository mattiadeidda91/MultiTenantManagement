using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Login;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Register;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Token;

namespace MultiTenantManagement.Abstractions.Services
{
    public interface IIdentityService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto);
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
    }
}
