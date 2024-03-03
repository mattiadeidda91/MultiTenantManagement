namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Rates.Request
{
    public class RequestRate
    {
        public Guid Id { get; set; }
        public double Daily { get; set; }
        public double Weekly { get; set; }
        public double Monthly { get; set; }
        public double Quarterly { get; set; }
        public double HalfYearly { get; set; }
        public double Annual { get; set; }
        public string[]? DayOfWeek { get; set; }

        public Guid ActivityId { get; set; }
    }
}
