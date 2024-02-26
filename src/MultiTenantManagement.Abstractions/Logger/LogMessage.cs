using Microsoft.Extensions.Logging;

namespace MultiTenantManagement.Abstractions.Logger
{
    //TODO: to complete this class and add and change log in the application
    public static partial class LogMessage
    {
        //To Log object properties use [LogProperties] of Microsoft.Extensions.Telemetry.Abstractions
        //[LoggerMessage(Level = LogLevel.Information, Message = "Crated Customer: {Customer}")]
        //public static partial void LogCreatedCustomerInfo(this ILogger logger, [LogProperties] CustomerDto customer);


        [LoggerMessage(Level = LogLevel.Information, Message = "Email '{Subject}' sent to {Recipients}")]
        public static partial void LogEmailSentInfo(this ILogger logger, string subject, string recipients);
    }
}
