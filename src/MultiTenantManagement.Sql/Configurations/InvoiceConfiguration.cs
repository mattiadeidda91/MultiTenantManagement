using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Sql.Configurations.Common;

namespace MultiTenantManagement.Sql.Configurations
{
    public class InvoiceConfiguration : BaseEntityConfiguration<Invoice> 
    {
        public override void Configure(EntityTypeBuilder<Invoice> builder)
        {
            base.Configure(builder);

            builder.ToTable("Invoices");

            builder.Property(e => e.InvoiceNumber).IsRequired();
            builder.Property(e => e.Price).IsRequired();
            builder.Property(e => e.PaymentDate).IsRequired();
        }
    }
}
