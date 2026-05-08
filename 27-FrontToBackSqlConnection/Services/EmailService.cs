using _27_FrontToBackSqlConnection.Configurations;
using Microsoft.Extensions.Options;

namespace _27_FrontToBackSqlConnection.Services;

public class EmailService : IEmailService
{
    private readonly AppSettings _appSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<AppSettings> appSettings, ILogger<EmailService> logger)
    {
        _appSettings = appSettings.Value;
        _logger = logger;
    }

    public Task SendAsync(string to, string subject, string body)
    {
        _logger.LogInformation(
            "Email sent from {SiteTitle} to {To}. Subject: {Subject}. Body: {Body}",
            _appSettings.SiteTitle,
            to,
            subject,
            body);

        return Task.CompletedTask;
    }

    public void SendMail()
    {
        _logger.LogInformation("Sent mail from {SiteTitle}", _appSettings.SiteTitle);
    }
}
