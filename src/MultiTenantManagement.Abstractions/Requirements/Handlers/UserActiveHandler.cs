﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MultiTenantManagement.Abstractions.Models.Entities.Authentication;
using System.Security.Claims;

namespace MultiTenantManagement.Abstractions.Requirements.Handlers
{
    public class UserActiveHandler : AuthorizationHandler<UserActiveRequirement>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserActiveHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Check if user is authenticated and not locked
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserActiveRequirement requirement)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(context.User);
            ArgumentNullException.ThrowIfNull(context.User.Identity);

            if (context.User.Identity.IsAuthenticated)
            {
                var id = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await userManager.FindByIdAsync(id);

                if (user != null)
                {
                    var isLocked = await userManager.IsLockedOutAsync(user);

                    if (!isLocked)
                        context.Succeed(requirement);
                }
            }
        }
    }
}
