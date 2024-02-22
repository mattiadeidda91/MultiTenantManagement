using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application
{
    public class HoursActivityDto : BaseDto
    {
        public string? Day { get; set; }
        public string? Hour { get; set; }
    }
}
