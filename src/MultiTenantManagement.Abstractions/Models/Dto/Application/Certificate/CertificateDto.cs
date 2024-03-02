using MultiTenantManagement.Abstractions.Models.Dto.Application.Customer;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Certificate
{
    public class CertificateDto : BaseDto
    {
        public string CertificateNumber { get; set; } = null!;
        public DateTime CertificateExpireDate { get; set; }
        public DateTime CertificateReleaseDate { get; set; }

        public CustomerWithoutCertificatesDto Customer { get; set; } = null!;
    }

    public class CertificateWithoutCustomerDto : BaseDto
    {
        public string CertificateNumber { get; set; } = null!;
        public DateTime CertificateExpireDate { get; set; }
        public DateTime CertificateReleaseDate { get; set; }
    }
}
