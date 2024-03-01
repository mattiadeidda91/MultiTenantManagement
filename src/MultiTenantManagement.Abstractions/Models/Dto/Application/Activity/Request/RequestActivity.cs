namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Activity.Request
{
    public class RequestActivity
    {
        public string Name { get; set; } = null!;
        public Guid SiteId { get; set; }
        public Guid CustomerId { get; set; }

        //TODO: Add Rates and Hours
    }
}
