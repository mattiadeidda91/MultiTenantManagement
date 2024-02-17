namespace MultiTenantManagement.Abstractions.Models.Dto
{
    public record TenantDto(Guid Id, string Name, string ConnectionString);
}
