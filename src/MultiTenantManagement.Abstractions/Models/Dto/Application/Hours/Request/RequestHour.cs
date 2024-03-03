namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Hours.Request
{
    public class RequestHour
    {
        public Guid Id { get; set; }
        public string? Day { get; set; }
        public string? Hour { get; set; }

        public Guid ActivityId { get; set; }
    }
}
