using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Sql.Configurations.Common;

namespace MultiTenantManagement.Sql.Configurations
{
    public class ExpenseConfiguration : BaseEntityConfiguration<Expense>
    {
        public override void Configure(EntityTypeBuilder<Expense> builder)
        {
            base.Configure(builder);

            builder.ToTable("Expenses");

            builder.Property(e => e.Price).IsRequired();
            builder.Property(e => e.PaymentDate).IsRequired();
            builder.Property(e => e.Recipient).HasMaxLength(200).IsRequired().IsUnicode(false);
            builder.Property(e => e.Description).HasMaxLength(int.MaxValue).IsUnicode(false);

        }
    }
}
