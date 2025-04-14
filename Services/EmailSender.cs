using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.UI.Services;
using PostmarkDotNet;

namespace SmartInventory3.Services;

public class EmailSender : IEmailSender
{
    private readonly string _postmarkKey;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<EmailSender> _logger;
    private readonly string _devOverrideEmail;

    public EmailSender(IConfiguration configuration, IWebHostEnvironment env, ILogger<EmailSender> logger)
    {
        _postmarkKey = configuration["Postmark:ApiKey"]
                       ?? throw new ArgumentNullException("Postmark API Key is missing");

        _devOverrideEmail = configuration["Postmark:DevOverrideTo"];
        _env = env;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        try
        {
            var client = new PostmarkClient(_postmarkKey);
            var msg = new PostmarkMessage
            {
                From = "SmartInventory <noreply@georgebrown.ca>",

                
                To = (_env.IsDevelopment() && !string.IsNullOrEmpty(_devOverrideEmail))
                    ? _devOverrideEmail
                    : email,

                Subject = subject,
                HtmlBody = message,
                TextBody = message
            };

            var response = await client.SendMessageAsync(msg);

            if (response.Status != PostmarkStatus.Success)
            {
                _logger.LogError("Failed to send email. Postmark error: {Message}", response.Message);
            }
            else
            {
                _logger.LogInformation("Email sent to {To} with subject '{Subject}'", msg.To, subject);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while sending email.");
            throw;
        }
    
    }
}