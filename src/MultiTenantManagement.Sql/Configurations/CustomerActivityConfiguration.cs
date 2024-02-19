using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Sql.Configurations.Common;

namespace MultiTenantManagement.Sql.Configurations
{
    public class CustomerActivityConfiguration : BaseEntityConfiguration<CustomerActivity>
    {
        public override void Configure(EntityTypeBuilder<CustomerActivity> builder)
        {
            base.Configure(builder);

            builder.ToTable("CustomersActivities");

            builder.HasKey(ur => new { ur.CustomerId, ur.ActivityId });

            builder.HasOne(ca => ca.Customer)
                .WithMany(c => c.CustomersActivities)
                .HasForeignKey(ca => ca.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ca => ca.Activity)
                .WithMany(c => c.CustomersActivities)
                .HasForeignKey(ca => ca.ActivityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
