using MultiTenantManagement.Abstractions.Models.Dto.Application.Customer;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.FederalCard
{
    public class FederalCardBaseDto : BaseDto
    {
        public string Card { get; set; } = null!;
        public DateTime MembershipDate { get; set; }
        public DateTime CardExpireDate { get; set; }
    }

    public class FederalCardDto : FederalCardBaseDto
    {
        public CustomerWithoutFederalCardsDto Customer { get; set; } = null!;
    }

    public class FederalCardWithoutCustomerDto : FederalCardBaseDto
    {
    }
}
