using Microsoft.Extensions.Logging;

namespace MultiTenantManagement.Abstractions.Logger
{
    //TODO: to complete this class and add and change log in the application
    public static partial class LogMessage
    {
        //To Log object properties use [LogProperties] of Microsoft.Extensions.Telemetry.Abstractions
        //[LoggerMessage(Level = LogLevel.Information, Message = "Crated Customer: {Customer}")]
        //public static partial void LogCreatedCustomerInfo(this ILogger logger, [LogProperties] CustomerDto customer);

        /* INFO */

        [LoggerMessage(Level = LogLevel.Information, Message = "Email '{Subject}' sent to {Recipients}")]
        public static partial void LogEmailSentInfo(this ILogger logger, string subject, string recipients);

        /* WARNING */

        [LoggerMessage(Level = LogLevel.Warning, Message = "Cannot delete database because not found: {Database}")]
        public static partial void LogDatabaseDeletionWarning(this ILogger logger, string database);
        
        [LoggerMessage(Level = LogLevel.Warning, Message = "Cannot delete tenant because not found: {Tenant}")]
        public static partial void LogTenantDeletionWarning(this ILogger logger, string tenant);

        /* ERROR */

        [LoggerMessage(Level = LogLevel.Error, Message = "Tenant Database creation error! {Message}")]
        public static partial void LogTenantCreationError(this ILogger logger, string message);

        [LoggerMessage(Level = LogLevel.Error, Message = "Cannot created database because it's already exists for database: {Database}")]
        public static partial void LogDatabaseCreationError(this ILogger logger, string database);
    }
}
