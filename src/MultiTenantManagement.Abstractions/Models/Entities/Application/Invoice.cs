namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string Header { get; set; } = null!;
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Provice { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string VatNumber { get; set; } = null!;
        public string? FiscalCode { get; set; }
    }
}
