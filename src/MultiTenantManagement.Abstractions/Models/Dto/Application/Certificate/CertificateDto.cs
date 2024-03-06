using MultiTenantManagement.Abstractions.Models.Dto.Application.Customer;
using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Certificate
{
    public class CertificateBaseDto : BaseDto
    {
        public string CertificateNumber { get; set; } = null!;
        public DateTime CertificateExpireDate { get; set; }
        public DateTime CertificateReleaseDate { get; set; }
    }

    public class CertificateDto : CertificateBaseDto
    {
        public CustomerWithoutCertificatesDto Customer { get; set; } = null!;
    }

    public class CertificateWithoutCustomerDto : CertificateBaseDto
    {
    }
}
