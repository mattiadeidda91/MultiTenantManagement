using MultiTenantManagement.Abstractions.Models.Dto.Application.Activity;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Rates
{
    public class RatesDto : BaseDto
    {
        //Add Property Active to Rates

        public double Daily { get; set; }
        public double Weekly { get; set; }
        public double Monthly { get; set; }
        public double Quarterly { get; set; }
        public double HalfYearly { get; set; }
        public double Annual { get; set; }
        public string[]? DayOfWeek { get; set; } //TDB

        public ActivityWithoutRatesDto Activity { get; set; } = null!;
    }

    public class RatesWithoutActivityDto : BaseDto
    {
        //Add Property Active to Rates

        public double Daily { get; set; }
        public double Weekly { get; set; }
        public double Monthly { get; set; }
        public double Quarterly { get; set; }
        public double HalfYearly { get; set; }
        public double Annual { get; set; }
        public string[]? DayOfWeek { get; set; } //TDB
    }
}
