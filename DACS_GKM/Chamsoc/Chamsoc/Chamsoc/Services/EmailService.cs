using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.Extensions.Logging;

namespace Chamsoc.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var host = _config["Smtp:Host"]; // e.g. smtp.mailtrap.io or smtp.gmail.com
                var port = int.TryParse(_config["Smtp:Port"], out var p) ? p : 587;
                var user = _config["Smtp:User"]; // username/login
                var pass = _config["Smtp:Pass"]; // password/app password
                var from = _config["Smtp:From"] ?? user; // display name or same as user
                var enableSsl = bool.TryParse(_config["Smtp:Ssl"], out var ssl) ? ssl : true;

                if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(from))
                {
                    _logger.LogError("SMTP config missing. Host: {Host}, User: {User}, From: {From}", host, user, from);
                    return false;
                }

                using var client = new SmtpClient(host, port)
                {
                    Credentials = new NetworkCredential(user, pass),
                    EnableSsl = enableSsl
                };

                using var message = new MailMessage(from, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                _logger.LogInformation("Sending email via {Host}:{Port} SSL={Ssl} From={From} To={To}", host, port, enableSsl, from, toEmail);
                await client.SendMailAsync(message);
                _logger.LogInformation("Email sent successfully to {To}", toEmail);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {To}", toEmail);
                return false;
            }
        }

        public async Task<bool> SendEmailWithInlineImageAsync(string toEmail, string subject, string htmlBody, byte[]? inlineImageBytes, string? inlineImageContentType, string inlineContentId = "inlineImage")
        {
            try
            {
                var host = _config["Smtp:Host"];
                var port = int.TryParse(_config["Smtp:Port"], out var p) ? p : 587;
                var user = _config["Smtp:User"];
                var pass = _config["Smtp:Pass"];
                var from = _config["Smtp:From"] ?? user;
                var enableSsl = bool.TryParse(_config["Smtp:Ssl"], out var ssl) ? ssl : true;

                if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(from))
                {
                    _logger.LogError("SMTP config missing. Host: {Host}, User: {User}, From: {From}", host, user, from);
                    return false;
                }

                using var client = new SmtpClient(host, port)
                {
                    Credentials = new NetworkCredential(user, pass),
                    EnableSsl = enableSsl
                };

                using var message = new MailMessage(from, toEmail)
                {
                    Subject = subject,
                };

                if (inlineImageBytes != null && inlineImageBytes.Length > 0 && !string.IsNullOrWhiteSpace(inlineImageContentType))
                {
                    var htmlView = AlternateView.CreateAlternateViewFromString(htmlBody.Replace("cid:inlineImage", $"cid:{inlineContentId}"), null, MediaTypeNames.Text.Html);
                    var stream = new System.IO.MemoryStream(inlineImageBytes);
                    var lr = new LinkedResource(stream, inlineImageContentType)
                    {
                        ContentId = inlineContentId,
                        TransferEncoding = TransferEncoding.Base64
                    };
                    htmlView.LinkedResources.Add(lr);
                    message.AlternateViews.Add(htmlView);
                }
                else
                {
                    message.Body = htmlBody;
                    message.IsBodyHtml = true;
                }

                _logger.LogInformation("Sending email with inline image via {Host}:{Port} SSL={Ssl} From={From} To={To}", host, port, enableSsl, from, toEmail);
                await client.SendMailAsync(message);
                _logger.LogInformation("Email with inline image sent successfully to {To}", toEmail);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email with inline image to {To}", toEmail);
                return false;
            }
        }
    }
}
