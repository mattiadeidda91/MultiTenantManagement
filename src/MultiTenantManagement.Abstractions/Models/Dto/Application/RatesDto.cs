using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application
{
    public class RatesDto : BaseDto
    {
        public double Daily { get; set; }
        public double Weekly { get; set; }
        public double Monthly { get; set; }
        public double Quarterly { get; set; }
        public double HalfYearly { get; set; }
        public double Annual { get; set; }
        public string[]? DayOfWeek { get; set; } //TDB

        public Guid ActivityId { get; set; }

        public ActivityDto Activity { get; set; } = null!;
    }
}
