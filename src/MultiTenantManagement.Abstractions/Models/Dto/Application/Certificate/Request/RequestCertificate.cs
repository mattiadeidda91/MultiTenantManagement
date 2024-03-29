﻿namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Certificate.Request
{
    public class RequestCertificate
    {
        public Guid Id { get; set; }
        public string CertificateNumber { get; set; } = null!;
        public DateTime CertificateExpireDate { get; set; }
        public DateTime CertificateReleaseDate { get; set; }

        public Guid CustomerId { get; set; }
    }
}
