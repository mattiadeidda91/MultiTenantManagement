﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantManagement.Abstractions.Models.Entities.Authentication;

namespace MultiTenantManagement.Abstractions.Extensions
{
    public static class IdentityBuilderExtension
    {
        public static IServiceCollection IdentityBuild<T>(this IServiceCollection services, bool isProduction) where T : DbContext
        {
            services.AddIdentity<ApplicationUser, ApplicationRole> (options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;

                options.SignIn.RequireConfirmedEmail = isProduction;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<T>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
