using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Invoice.Request
{
    public class RequestInvoice
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string InvoiceNumber { get; set; } = null!;

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public double Total { get; set; }

        [Required]
        public Guid SiteId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        //[Required]
        //public Guid ActivityId { get; set; }

        [Required]
        public Guid RateId { get; set; }
    }
}
