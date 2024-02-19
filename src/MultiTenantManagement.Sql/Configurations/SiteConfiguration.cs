using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Sql.Configurations.Common;

namespace MultiTenantManagement.Sql.Configurations
{
    public class SiteConfiguration : BaseEntityConfiguration<Site>
    {
        public override void Configure(EntityTypeBuilder<Site> builder)
        {
            base.Configure(builder);

            builder.ToTable("Sites");

            builder.Property(s => s.Name).HasMaxLength(200).IsRequired().IsUnicode(false);
        }
    }
}
