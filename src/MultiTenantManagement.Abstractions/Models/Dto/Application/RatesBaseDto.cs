using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application
{
    public class RatesBaseDto : BaseDto
    {
        public double Monthly { get; set; }
        public double Quarterly { get; set; }
        public double HalfYearly { get; set; }
        public double Annual { get; set; }
        public int? DayOfWeek { get; set; } //TDB
    }
}
