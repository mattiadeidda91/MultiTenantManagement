using MultiTenantManagement.Abstractions.Models.Dto.Application.Site;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Expense
{
    public class ExpenseBaseDto : BaseDto
    {
        public DateTime PaymentDate { get; set; }
        public string? Description { get; set; }
        public string Recipient { get; set; } = null!;
        public double Price { get; set; }
        public string? ActivityName { get; set; }
    }

    public class ExpenseDto : ExpenseBaseDto
    {
        public virtual SiteDto Site { get; set; } = null!;
    }

    public class ExpenseWithoutSiteDto : ExpenseBaseDto
    {
    }
}
