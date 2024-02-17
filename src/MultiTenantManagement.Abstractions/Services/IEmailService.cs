namespace MultiTenantManagement.Abstractions.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string body, List<string> toEmails, List<string>? ccEmails = null, List<string>? bccEmails = null);
    }
}
