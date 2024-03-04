using MultiTenantManagement.Abstractions.Models.Dto.Application.Activity;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Hours
{
    public class HoursDto : BaseDto
    {
        public string? Day { get; set; }
        public string? Hour { get; set; }

        public ActivityWithoutHoursDto Activity { get; set; } = null!;
    }

    public class HoursWithoutActivityDto : BaseDto
    {
        public string? Day { get; set; }
        public string? Hour { get; set; }
    }
}
