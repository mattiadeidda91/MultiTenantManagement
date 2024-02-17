using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Entities;
using MultiTenantManagement.Abstractions.Models.Entities.Common;
using MultiTenantManagement.Abstractions.Services;
using System.Reflection;

#pragma warning disable S2219 // Runtime type checking should be simplified
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace MultiTenantManagement.Sql.DatabaseContext
{
    public class ApplicationDbContext : BaseDbContext, IApplicationDbContext
    {
        private readonly Guid tenantId;

        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserService userService) 
            : base(options)
        {
            tenantId = userService.GetTenantId();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => typeof(TenantEntity).IsAssignableFrom(e.Entity.GetType()));

            foreach (var entry in entries.Where(e => e.State is EntityState.Added or EntityState.Modified))
            {
                (entry.Entity as TenantEntity).TenantId = tenantId;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entities = modelBuilder.Model.GetEntityTypes()
                .Where(t => typeof(TenantEntity).IsAssignableFrom(t.ClrType)).ToList();

            foreach (var type in entities.Select(t => t.ClrType))
            {
                var methods = SetGlobalQueryFiltersMethod(type);
                foreach (var method in methods)
                {
                    var genericMethod = method.MakeGenericMethod(type);
                    genericMethod.Invoke(this, new object[] { modelBuilder });
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        private static IEnumerable<MethodInfo> SetGlobalQueryFiltersMethod(Type type)
        {
            var result = new List<MethodInfo>();

            if (typeof(TenantEntity).IsAssignableFrom(type))
            {
                result.Add(setQueryFilterOnTenantEntity);
            }

            return result;
        }

        private static readonly MethodInfo setQueryFilterOnTenantEntity = typeof(ApplicationDbContext)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == nameof(SetQueryFilterOnTenantEntity));

        private void SetQueryFilterOnTenantEntity<T>(ModelBuilder builder) where T : TenantEntity
            => builder.Entity<T>().HasQueryFilter(e => e.TenantId == tenantId);
    }
}

#pragma warning restore S2219 // Runtime type checking should be simplified
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
#pragma warning restore CS8602 // Dereference of a possibly null reference.
