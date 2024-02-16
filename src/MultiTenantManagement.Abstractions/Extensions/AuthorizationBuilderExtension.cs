using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantManagement.Abstractions.Requirements;

namespace MultiTenantManagement.Abstractions.Extensions
{
    public static class AuthorizationBuilderExtension
    {
        public static IServiceCollection AuthorizationBuild(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                var policyBuilder = new AuthorizationPolicyBuilder().RequireAuthenticatedUser();
                policyBuilder.Requirements.Add(new UserActiveRequirement());
                options.FallbackPolicy = options.DefaultPolicy = policyBuilder.Build();

                options.AddPolicy("UserActive", policy =>
                {
                    policy.Requirements.Add(new UserActiveRequirement());
                });
            });

            return services;
        }
    }
}
