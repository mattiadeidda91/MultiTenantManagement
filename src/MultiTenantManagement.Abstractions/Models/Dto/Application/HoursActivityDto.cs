using MultiTenantManagement.Abstractions.Models.Dto.Application.Activity;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application
{
    public class HoursActivityDto : BaseDto
    {
        //TODO: Check if to use this class instead Rates.DayOfWeek, maybe to change DayOfWeek with reference to HoursActivity with many to many relationship
        public string? Day { get; set; }
        public string? Hour { get; set; }

        public Guid ActivityId { get; set; }
        public ActivityDto Activity { get; set; } = null!;
    }
}
