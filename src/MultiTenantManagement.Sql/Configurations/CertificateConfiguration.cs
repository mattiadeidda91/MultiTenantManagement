using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Sql.Configurations.Common;

namespace MultiTenantManagement.Sql.Configurations
{
    public class CertificateConfiguration : BaseEntityConfiguration<Certificate>
    {
        public override void Configure(EntityTypeBuilder<Certificate> builder)
        {
            base.Configure(builder);

            builder.ToTable("Certificates");

            builder.Property(c => c.CertificateNumber).HasMaxLength(100).IsRequired().IsUnicode(false);
            builder.Property(c => c.CertificateExpireDate).HasMaxLength(10).HasColumnType("datetime").IsRequired();

            //builder.HasOne(c => c.Customer)
            //    .WithMany(s => s.Certificates)
            //    .HasForeignKey(c => c.CustomerId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
