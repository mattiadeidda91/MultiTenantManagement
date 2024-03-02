using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Sql.Configurations.Common;

namespace MultiTenantManagement.Sql.Configurations
{
    public class MembershipCardConfiguration : BaseEntityConfiguration<MembershipCard>
    {
        public override void Configure(EntityTypeBuilder<MembershipCard> builder)
        {
            base.Configure(builder);

            builder.ToTable("MembershipCards");

            builder.Property(f => f.Card).HasMaxLength(150).IsRequired().IsUnicode(false);
            builder.Property(f => f.MembershipDate).HasMaxLength(10).HasColumnType("datetime").IsRequired();
            builder.Property(f => f.CardExpireDate).HasMaxLength(10).HasColumnType("datetime").IsRequired();

            builder.HasOne(f => f.Customer)
               .WithMany(c => c.MembershipCards)
               .HasForeignKey(f => f.CustomerId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
