using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application
{
    public class RatesDto : BaseDto
    {
        public double Price { get; set; } //TBD: it was string Rates, mayby it's Name
        public int? DayOfWeek { get; set; } //TBD
    }
}
