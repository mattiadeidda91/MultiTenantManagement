using Microsoft.AspNetCore.Builder;
using MultiTenantManagement.Abstractions.Middleware;

namespace MultiTenantManagement.Abstractions.Extensions
{
    public static class UseExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
