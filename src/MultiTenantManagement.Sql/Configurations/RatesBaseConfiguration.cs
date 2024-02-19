using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Sql.Configurations.Common;

namespace MultiTenantManagement.Sql.Configurations
{
    public class RatesBaseConfiguration : BaseEntityConfiguration<RatesBase>
    {
        public override void Configure(EntityTypeBuilder<RatesBase> builder)
        {
            base.Configure(builder);

            builder.ToTable("RatesBase");

            //builder.HasOne(r => r.Activity)
            //    .WithMany(a => a.RatesBase)
            //    .HasForeignKey(r => r.ActivityId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
