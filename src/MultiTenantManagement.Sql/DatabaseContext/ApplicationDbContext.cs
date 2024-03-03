using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Abstractions.Models.Entities.Common;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Sql.DatabaseContext.Common;
using System.Reflection;

#pragma warning disable S2219 // Runtime type checking should be simplified
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace MultiTenantManagement.Sql.DatabaseContext
{
    public class ApplicationDbContext : BaseDbContext, IApplicationDbContext
    {
        private readonly Guid tenantId;

        //public DbSet<Product> Products { get; set; } //TODO: remove it

        //public virtual DbSet<AnnoCorrente> AnnoCorrente { get; set; } = null!;
        public virtual DbSet<Activity> Activities { get; set; } = null!;
        //public virtual DbSet<Calendario> Calendario { get; set; } = null!;
        //public virtual DbSet<Cassa> Cassa { get; set; } = null!;
        public virtual DbSet<Certificate> Certificates { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerActivity> CustomersActivities { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        //public virtual DbSet<FattureStampate> FattureStampate { get; set; } = null!;
        public virtual DbSet<Hours> Hours { get; set; } = null!;
        //public virtual DbSet<ScadenzeAttività> ScadenzeAttività { get; set; } = null!;
        public virtual DbSet<Site> Sites { get; set; } = null!;
        public virtual DbSet<Rates> Rates { get; set; } = null!;
        public virtual DbSet<FederalCard> FederalCards { get; set; } = null!;
        public virtual DbSet<MembershipCard> MembershipCards { get; set; } = null!;
        //public virtual DbSet<Expense> Expenses { get; set; } = null!;

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

            var currentTime = DateTime.UtcNow;

            foreach (var entry in entries.Where(e => e.State is EntityState.Added or EntityState.Modified))
            {
                (entry.Entity as TenantEntity).TenantId = tenantId;

                if (entry.State is EntityState.Added)
                {
                    entry.Property("InsertDate").CurrentValue = currentTime;
                }
                else
                {
                    entry.Property("InsertDate").IsModified = false;
                }

                entry.Property("UpdateDate").CurrentValue = currentTime;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Get All EntityConfigurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //To check if move this in BaseEntityConfiguration
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
