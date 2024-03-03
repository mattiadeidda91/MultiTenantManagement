using MultiTenantManagement.Abstractions.Models.Dto.Application.Activity;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Hours
{
    public class HoursDto : BaseDto
    {
        //TODO: Check if to use this class instead Rates.DayOfWeek, maybe to change DayOfWeek with reference to Hours with many to many relationship
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
