using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Authentication.Tenant
{
    public class CreateTenantResponse : ResponseBaseDto
    {
        public TenantDto? Tenant { get; set; }
    }
}
