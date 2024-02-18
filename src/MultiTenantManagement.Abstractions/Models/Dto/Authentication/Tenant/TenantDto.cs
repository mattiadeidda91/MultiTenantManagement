namespace MultiTenantManagement.Abstractions.Models.Dto.Authentication.Tenant
{
    public record TenantDto(Guid Id, string Name, string ConnectionString);
}
