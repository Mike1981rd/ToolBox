using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ToolBox.Interfaces;

namespace ToolBox.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // For development - just log the email
            if (_configuration.GetValue<bool>("EmailSettings:UseDevelopmentMode", true))
            {
                _logger.LogInformation("Development Mode - Email would be sent to: {Email}", email);
                _logger.LogInformation("Subject: {Subject}", subject);
                _logger.LogInformation("Body: {Body}", htmlMessage);
                return;
            }

            // For production - use SMTP
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                var host = smtpSettings["Server"];
                var port = int.Parse(smtpSettings["Port"] ?? "587");
                var enableSsl = bool.Parse(smtpSettings["EnableSsl"] ?? "true");
                var userName = smtpSettings["Username"];
                var password = smtpSettings["Password"];
                var fromAddress = smtpSettings["FromAddress"];
                var fromName = smtpSettings["FromName"];

                using var client = new SmtpClient(host)
                {
                    Port = port,
                    Credentials = new NetworkCredential(userName, password),
                    EnableSsl = enableSsl,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromAddress, fromName),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
                _logger.LogInformation("Email sent successfully to {Email}", email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to {Email}", email);
                throw;
            }
        }
    }
}