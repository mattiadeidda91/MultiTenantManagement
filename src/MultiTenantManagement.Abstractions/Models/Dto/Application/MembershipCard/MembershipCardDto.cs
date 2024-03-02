using MultiTenantManagement.Abstractions.Models.Dto.Application.Customer;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.MembershipCard
{
    public class MembershipCardDto : BaseDto
    {
        public string Card { get; set; } = null!;
        public DateTime MembershipDate { get; set; }
        public DateTime CardExpireDate { get; set; }

        public CustomerWithoutMembershipCardsDto Customer { get; set; } = null!;
    }

    public class MembershipCardWithoutCustomerDto : BaseDto
    {
        public string Card { get; set; } = null!;
        public DateTime MembershipDate { get; set; }
        public DateTime CardExpireDate { get; set; }
    }
}
