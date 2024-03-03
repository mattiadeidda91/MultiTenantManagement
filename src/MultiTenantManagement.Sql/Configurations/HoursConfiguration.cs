using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Sql.Configurations.Common;

namespace MultiTenantManagement.Sql.Configurations
{
    public class HoursConfiguration : BaseEntityConfiguration<Hours> 
    {
        public override void Configure(EntityTypeBuilder<Hours> builder)
        {
            base.Configure(builder);

            builder.ToTable("Hours");

            builder.Property(h => h.Day).HasMaxLength(30).IsRequired().IsUnicode(false);
            builder.Property(h => h.Hour).HasMaxLength(30).IsRequired().IsUnicode(false);

            builder.HasOne(h => h.Activity)
                .WithMany(a => a.Hours)
                .HasForeignKey(h => h.ActivityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
