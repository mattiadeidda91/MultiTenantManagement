using MultiTenantManagement.Abstractions.Models.Dto;

namespace MultiTenantManagement.Abstractions.Services
{
    public interface IIdentityService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto);
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
    }
}
