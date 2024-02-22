using MultiTenantManagement.Abstractions.Models.Dto.Common;

namespace MultiTenantManagement.Abstractions.Models.Dto.Application
{
    public class CertificateDto : BaseDto
    {
        public string CertificateNumber { get; set; } = null!;
        public DateTime CertificateExpireDate { get; set; }
    }
}
