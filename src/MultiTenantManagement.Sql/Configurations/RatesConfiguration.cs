using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Sql.Configurations.Common;
using System.Text.Json;

namespace MultiTenantManagement.Sql.Configurations
{
    public class RatesConfiguration : BaseEntityConfiguration<Rates>
    {
        public override void Configure(EntityTypeBuilder<Rates> builder)
        {
            base.Configure(builder);

            builder.ToTable("Rates");

            builder.Property(r => r.DayOfWeek)
                .HasMaxLength(int.MaxValue)
                .HasConversion(
                    c => JsonSerializer.Serialize(c, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }),
                    c => JsonSerializer.Deserialize<string[]>(c, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                );

            builder.HasOne(r => r.Activity)
                .WithMany(a => a.Rates)
                .HasForeignKey(r => r.ActivityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
