using MultiTenantManagement.Abstractions.Models.Dto.Application.Activity;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Customer;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Rates;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Site;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Invoice
{
    public class InvoiceBaseDto : BaseDto
    {
        public string InvoiceNumber { get; set; } = null!;
        public DateTime PaymentDate { get; set; }
        public double Total { get; set; }
    }

    public class InvoiceDto : InvoiceBaseDto
    {
        public SiteDto Site { get; set; } = null!;

        public CustomerBaseDto Customer { get; set; } = null!;

        //public ActivityBaseDto Activity { get; set; } = null!;

        public RatesDto Rate { get; set; } = null!;
    }
}
