namespace MultiTenantManagement.Abstractions.Models.Dto.Application.FederalCard.Request
{
    public class RequestFederalCard
    {
        public string Card { get; set; } = null!;
        public DateTime MembershipDate { get; set; }
        public DateTime CardExpireDate { get; set; }

        public Guid CustomerId { get; set; }
    }
}
