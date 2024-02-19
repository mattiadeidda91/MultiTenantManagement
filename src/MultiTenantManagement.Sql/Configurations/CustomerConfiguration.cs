using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Sql.Configurations.Common;

namespace MultiTenantManagement.Sql.Configurations
{
    public class CustomerConfiguration : BaseEntityConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);

            builder.ToTable("Customers");

            builder.Property(u => u.BirthDate).HasMaxLength(10).HasColumnType("datetime");
            builder.Property(u => u.Email).HasMaxLength(100).IsRequired().IsUnicode(false);
            builder.Property(u => u.FirstName).HasMaxLength(100).IsRequired().IsUnicode(false);
            builder.Property(u => u.LastName).HasMaxLength(100).IsRequired().IsUnicode(false);
            builder.Property(u => u.Phone).HasMaxLength(30).IsUnicode(false);
            builder.Property(u => u.MobilePhone).HasMaxLength(30).IsUnicode(false);
            builder.Property(u => u.City).HasMaxLength(150).IsUnicode(false);
            builder.Property(u => u.Region).HasMaxLength(100).IsUnicode(false);
            builder.Property(u => u.Province).HasMaxLength(100).IsUnicode(false);
            builder.Property(u => u.Address).HasMaxLength(200).IsUnicode(false);
            builder.Property(u => u.PostalCode).HasMaxLength(10).IsUnicode(false);
            builder.Property(u => u.BirthPlace).HasMaxLength(150).IsUnicode(false);
            builder.Property(u => u.BirthProvince).HasMaxLength(100).IsUnicode(false);
            builder.Property(u => u.FiscalCode).HasMaxLength(20).IsUnicode(false);
            builder.Property(u => u.Gender).HasMaxLength(1).IsRequired().IsUnicode(false);
            builder.Property(u => u.Note).HasMaxLength(int.MaxValue).IsUnicode(false);

            //builder.HasOne(c => c.Site)
            //    .WithMany(s => s.Customers)
            //    .HasForeignKey(c => c.SiteId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
