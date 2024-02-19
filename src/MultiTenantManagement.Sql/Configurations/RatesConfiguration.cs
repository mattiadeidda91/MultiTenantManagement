using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Sql.Configurations.Common;

namespace MultiTenantManagement.Sql.Configurations
{
    public class RatesConfiguration : BaseEntityConfiguration<Rates>
    {
        public override void Configure(EntityTypeBuilder<Rates> builder)
        {
            base.Configure(builder);

            builder.ToTable("Rates");

            //builder.HasOne(r => r.Activity)
            //    .WithMany(s => s.Rates)
            //    .HasForeignKey(r => r.ActivityId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
