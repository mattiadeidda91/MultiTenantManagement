using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MultiTenantManagement.Abstractions.Services;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Entities.Authentication;

namespace MultiTenantManagement.Sql.DatabaseContext.Common
{
    public abstract class BaseIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>, ApplicationUserRole,
            IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, IRepository
    {
        protected BaseIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        public IQueryable<T> GetData<T>(bool trackingChanges = false, bool ignoreQueryFilters = false) where T : class
        {
            var set = Set<T>().AsQueryable();

            if (ignoreQueryFilters)
            {
                set = set.IgnoreQueryFilters();
            }

            return trackingChanges ? set.AsTracking() : set.AsNoTrackingWithIdentityResolution();
        }

        public ValueTask<T?> GetAsync<T>(params object[] keyValues) where T : class
            => Set<T>().FindAsync(keyValues);

        public void Insert<T>(T entity) where T : class
            => Set<T>().Add(entity);

        public void Update<T>(T entity) where T : class
            => Set<T>().Add(entity);

        public void Delete<T>(T entity) where T : class
            => Set<T>().Remove(entity);

        public void Delete<T>(IEnumerable<T> entities) where T : class
            => Set<T>().RemoveRange(entities);

        public Task SaveAsync()
            => SaveChangesAsync();

        public Task ExecuteTransactionAsync(Func<Task> action)
        {
            var strategy = Database.CreateExecutionStrategy();

            return strategy.ExecuteAsync(async () =>
            {
                using var transaction = await Database.BeginTransactionAsync().ConfigureAwait(false);
                await action.Invoke().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);
            });
        }
    }
}
