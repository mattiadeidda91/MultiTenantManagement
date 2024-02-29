using MultiTenantManagement.Abstractions.Models.Entities.Common;

namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    public class Certificate : TenantEntity
    {
        public string? CertificateNumber { get; set; }
        public DateTime CertificateReleaseDate { get; set; }
        public DateTime CertificateExpireDate { get; set; }
        public Guid CustomerId { get; set; }

        //public virtual Customer Customer { get; set; } = null!;
    }
}
