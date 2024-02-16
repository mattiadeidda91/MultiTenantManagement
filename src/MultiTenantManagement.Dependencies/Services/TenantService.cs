using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MultiTenantManagement.Abstractions.Models.Dto;
using MultiTenantManagement.Abstractions.Models.Entities;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities;
using MultiTenantManagement.Sql.DatabaseContext;

namespace MultiTenantManagement.Dependencies.Services
{
    public class TenantService : ITenantService
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        private readonly IMemoryCache cache;
        private readonly IMapper mapper;
        private readonly ILogger<TenantService> logger;
        private readonly AuthenticationDbContext authenticationDbContext;

        public TenantService(AuthenticationDbContext authenticationDbContext, IUserService userService, IConfiguration configuration, IMemoryCache cache, IMapper mapper, ILogger<TenantService> logger)
        {
            this.authenticationDbContext = authenticationDbContext;
            this.userService = userService;
            this.configuration = configuration;
            this.cache = cache;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<CreateTenantResponse> CreateTenantAsync(Guid tenantId)
        {
            //Create new Tenant
            if (tenantId == Guid.Empty)
            {
                var databaseName = $"Tenant_{GenerateTenantName()}";

                var connTemplate = configuration.GetConnectionString(ConnectionStrings.TenantConnectionTemplate);
                var connectionString = string.Format(connTemplate!, databaseName);

                var tenant = new Tenant()
                {
                    //Cannot use User Id and Password, so leave like this and use the admin authentication(same credential for AuthDbContext) for prod
                    ConnectionString = connectionString
                };

                authenticationDbContext.Tenants.Add(tenant);

                await authenticationDbContext.SaveChangesAsync();

                var isCreated = await CreateTenantDatabase(tenant.ConnectionString);

                if (!isCreated)
                    logger.LogError("Cannot created database because it's already exists for database: {Database}", databaseName);

                return new CreateTenantResponse
                {
                    IsSuccess = isCreated,
                    TenantId = tenant.Id,
                    Errors = !isCreated ? new List<string>() { "Cannot created database because it's already exists for database: {Database}", databaseName } : null
                };
            }
            else
            {
                //Get existing Tenant
                var checkTenantExist = await GetTenantByIdAsync(tenantId);

                return new CreateTenantResponse()
                {
                    IsSuccess = checkTenantExist != null,
                    TenantId = checkTenantExist?.Id ?? Guid.Empty,
                    Errors = checkTenantExist == null ? new List<string>() { $"Register Failed! Tenant with Id: {tenantId} doesn't exist" } : null
                };
            }
        }

        public async Task<TenantDto?> GetTenantByIdAsync(Guid tenantId)
        {
            if(cache.TryGetValue<TenantDto>(tenantId, out var tenant))
            {
                return tenant;
            }

            var tenantDb = await authenticationDbContext.Tenants.FirstOrDefaultAsync(t => t.Id == tenantId);

            return mapper.Map<TenantDto>(tenantDb);
        }

        public TenantDto GetTenantFromAuthenticatedUser()
        {
            var tenants = cache.GetOrCreate(CacheKeys.tenants, entry =>
            {
                var tenants = authenticationDbContext.Tenants?.ToList()?.ToDictionary(k => k.Id, v => new TenantDto(v.Id, v.ConnectionString!));

                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return tenants;
            });

            var tenantId = userService.GetTenantId();
            if (tenants!.TryGetValue(tenantId, out var tenant))
            {
                return tenant;
            }

            return null;
        }

        public void ClearCache() => cache.Remove(CacheKeys.tenants);

        private async Task<bool> CreateTenantDatabase(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var successCreation = await dbContext.Database.EnsureCreatedAsync();

            if(successCreation)
                await dbContext.Database.MigrateAsync();

            ClearCache();

            return successCreation;
        }

        private static string GenerateTenantName()
        {
            return Utils.GenerateRamdomString(10, true);
        }
    }
}
