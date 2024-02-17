using MultiTenantManagement.Abstractions.Models.Dto;

namespace MultiTenantManagement.Abstractions.Services
{
    public interface ITenantService
    {
        TenantDto GetTenantFromAuthenticatedUser();
        Task<TenantDto?> GetTenantByIdAsync(Guid tenantId);
        Task<CreateTenantResponse> CreateTenantAsync(Guid tenantId);
        Task<bool> DeleteTenantAsync(TenantDto? tenant);
        void ClearCache();
    }
}
