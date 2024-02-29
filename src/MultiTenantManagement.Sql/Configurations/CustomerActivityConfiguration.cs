using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantManagement.Abstractions.Models.Entities.Application;

namespace MultiTenantManagement.Sql.Configurations
{
    public class CustomerActivityConfiguration : IEntityTypeConfiguration<CustomerActivity>
    {
        public void Configure(EntityTypeBuilder<CustomerActivity> builder)
        {
            builder.ToTable("CustomersActivities");

            builder.HasKey(ur => new { ur.CustomerId, ur.ActivityId });

            builder.HasOne(ca => ca.Customer)
                .WithMany(c => c.CustomersActivities)
                .HasForeignKey(ca => ca.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ca => ca.Activity)
                .WithMany(c => c.CustomersActivities)
                .HasForeignKey(ca => ca.ActivityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
