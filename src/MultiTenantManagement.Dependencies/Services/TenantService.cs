using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Tenant;
using MultiTenantManagement.Abstractions.Models.Entities.Authentication;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities;
using MultiTenantManagement.Abstractions.Utilities.Costants;
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
        private readonly IAuthenticationDbContext authenticationDbContext;

        public TenantService(IAuthenticationDbContext authenticationDbContext, IUserService userService, IConfiguration configuration, IMemoryCache cache, IMapper mapper, ILogger<TenantService> logger)
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
                (var databaseName, var connectionString) = GenerateConnectionString();

                var tenant = new Tenant()
                {
                    //Cannot use User Id and Password in the connectionString,
                    //so leave like this and use the admin authentication(same credential for AuthDbContext) for prod
                    ConnectionString = connectionString,
                    Name= databaseName
                };

                authenticationDbContext.Insert(tenant);

                await authenticationDbContext.SaveAsync();

                var isCreated = await CreateTenantDatabaseAsync(tenant.ConnectionString);

                if (!isCreated)
                    logger.LogError("Cannot created database because it's already exists for database: {Database}", databaseName);

                return new CreateTenantResponse
                {
                    IsSuccess = isCreated,
                    Tenant = mapper.Map<TenantDto>(tenant),
                    Errors = !isCreated ? new List<string>() { "Cannot created database because it's already exists for database: {Database}", databaseName } : null
                };
            }
            else
            {
                //Get existing Tenant
                var dbTenant = await GetTenantByIdAsync(tenantId);

                return new CreateTenantResponse()
                {
                    IsSuccess = dbTenant != null,
                    Tenant = dbTenant,
                    Errors = dbTenant == null ? new List<string>() { $"Register Failed! Tenant with Id: {tenantId} doesn't exist" } : null
                };
            }
        }

        public async Task<bool> DeleteTenantAsync(TenantDto? tenant)
        {
            //TODO: remove association in Tenants table of IdentityAuthentication DB before to remove the database using the IdentityAuthentication connectionString

            var deleteResult = false;

            if (tenant != null)
            {
                deleteResult = await DeleteTenantDatabaseAsync(tenant.ConnectionString);

                if (!deleteResult)
                    logger.LogWarning("Cannot delete database because not found: {Database}", tenant.Name);
            }

            return deleteResult;
        }

        public async Task<TenantDto?> GetTenantByIdAsync(Guid tenantId)
        {
            if(cache.TryGetValue<TenantDto>(tenantId, out var tenant))
            {
                return tenant;
            }

            var tenantDb = await authenticationDbContext.GetData<Tenant>().FirstOrDefaultAsync(t => t.Id == tenantId);
            //var tenantDb = await authenticationDbContext.Tenants.FirstOrDefaultAsync(t => t.Id == tenantId);

            return mapper.Map<TenantDto>(tenantDb);
        }

        public TenantDto GetTenantFromAuthenticatedUser()
        {
            var tenants = cache.GetOrCreate(CacheKeys.tenants, entry =>
            {
                var tenants = authenticationDbContext.GetData<Tenant>().ToList()?.ToDictionary(k => k.Id, v => new TenantDto(v.Id, v.Name!, v.ConnectionString!));

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

        private async Task<bool> DeleteTenantDatabaseAsync(string connectionString)
        {
            var optionsBuilder = GeneratebContextOptionsBuilder(connectionString);

            using var dbContext = new ApplicationDbContext(optionsBuilder);

            var successDeletion = await dbContext.Database.EnsureDeletedAsync();

            ClearCache();

            return successDeletion;
        }

        public void ClearCache() 
            => cache.Remove(CacheKeys.tenants);

        private async Task<bool> CreateTenantDatabaseAsync(string connectionString)
        {
            var optionsBuilder = GeneratebContextOptionsBuilder(connectionString);

            using var dbContext = new ApplicationDbContext(optionsBuilder);
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

        private (string databaseName, string connectionString) GenerateConnectionString()
        {
            var databaseName = $"Tenant_{GenerateTenantName()}";

            var connTemplate = configuration.GetConnectionString(ConnectionStrings.TenantConnectionTemplate)!;

            return (databaseName, string.Format(connTemplate, databaseName));
        } 

        private DbContextOptions<ApplicationDbContext> GeneratebContextOptionsBuilder(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return optionsBuilder.Options;
        }
    }
}
