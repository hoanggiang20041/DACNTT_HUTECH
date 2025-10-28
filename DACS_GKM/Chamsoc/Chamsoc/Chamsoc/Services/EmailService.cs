using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Chamsoc.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
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

                await client.SendMailAsync(message);
                return true;
            }
            catch
            {
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
                    // Create HTML view with LinkedResource (cid)
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

                await client.SendMailAsync(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}


