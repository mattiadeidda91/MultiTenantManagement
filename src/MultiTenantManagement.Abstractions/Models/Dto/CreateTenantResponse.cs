namespace MultiTenantManagement.Abstractions.Models.Dto
{
    public class CreateTenantResponse
    {
        public Guid TenantId { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
