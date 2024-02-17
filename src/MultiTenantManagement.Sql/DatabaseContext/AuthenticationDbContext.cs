using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Entities;
using MultiTenantManagement.Abstractions.Services;

namespace MultiTenantManagement.Sql.DatabaseContext
{
    public class AuthenticationDbContext : BaseIdentityDbContext, IAuthenticationDbContext
    {
        public DbSet<Tenant> Tenants { get; set; }

        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(user =>
            {
                user.Property(u => u.FirstName).HasMaxLength(256).IsRequired();
                user.Property(u => u.LastName).HasMaxLength(256);
            });

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles).HasForeignKey(r=> r.RoleId).IsRequired();
                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles).HasForeignKey(r => r.UserId).IsRequired();
            });

            builder.Entity<Tenant>(tenant =>
            {
                tenant.ToTable("Tenants");
                tenant.HasKey(t => t.Id);

                tenant.Property(t => t.Id).ValueGeneratedOnAdd();
                tenant.Property(t => t.Name).IsRequired();
                tenant.Property(t => t.ConnectionString).HasMaxLength(4000).IsRequired().IsUnicode(false);
            });
        }
    }
}
