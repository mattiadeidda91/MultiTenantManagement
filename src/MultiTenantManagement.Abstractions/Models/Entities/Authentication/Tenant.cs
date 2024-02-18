namespace MultiTenantManagement.Abstractions.Models.Entities.Authentication
{
    public class Tenant
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? ConnectionString { get; set; }
    }
}
