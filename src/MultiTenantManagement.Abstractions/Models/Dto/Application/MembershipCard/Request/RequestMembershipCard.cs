namespace MultiTenantManagement.Abstractions.Models.Dto.Application.MembershipCard.Request
{
    public class RequestMembershipCard
    {
        public string Card { get; set; } = null!;
        public DateTime MembershipDate { get; set; }
        public DateTime CardExpireDate { get; set; }

        public Guid CustomerId { get; set; }
    }
}
