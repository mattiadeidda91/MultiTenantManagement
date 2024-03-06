using MultiTenantManagement.Abstractions.Models.Entities.Common;

namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    //public class InvoiceHeader //TODO: Retrieve this information during registration user and add them to DB
    //{
    //    public Guid Id { get; set; }
    //    public string Header { get; set; } = null!;
    //    public string? City { get; set; }
    //    public string? Address { get; set; }
    //    public string? Provice { get; set; }
    //    public string? Region { get; set; }
    //    public string? PostalCode { get; set; }
    //    public string VatNumber { get; set; } = null!;
    //    public string? FiscalCode { get; set; }
    //}

    public class Invoice : TenantEntity
    {
        public string InvoiceNumber { get; set; } = null!;
        public DateTime PaymentDate { get; set; }
        public double Total { get; set; }

        //TODO: to manage the Header Invoice -> Save it in IIdentityAuthentication.AspNetUsers table the InvoiceHeaderId and create new table InvoiceHeader with the fields of the header and UserId foreign key

        public Guid SiteId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ActivityId { get; set; }
        public Guid RateId { get; set; }

        public virtual Site? Site { get; set; }
        public virtual Customer? Customer { get; set; }
        //public virtual Activity? Activity { get; set; }
        public virtual Rates Rate { get; set; } = null!; //TODO: to manage the multiple Rates and Activities for the same Invoice (new table many to many)
    }
}
