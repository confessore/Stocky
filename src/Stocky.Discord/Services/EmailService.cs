using Microsoft.Extensions.Options;
using Stocky.Discord.Services.Interfaces;
using Stocky.Discord.Services.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Stocky.Discord.Services
{
    public class EmailService : IEmailService
    {
        readonly EmailSenderOptions options;

        public EmailService(
            IOptions<EmailSenderOptions> options)
        {
            this.options = options.Value;
        }

        public async Task SendEmailAsync(string recipient, string subject, string body) =>
            await ExecuteAsync(recipient, subject, body);

        async Task ExecuteAsync(string recipient, string subject, string body)
        {
            var smtpClient = new SmtpClient
            {
                Host = options.SmtpHost,
                Port = Convert.ToInt32(options.SmtpPort),
                EnableSsl = true,
                Credentials = new NetworkCredential(options.SmtpUser, options.SmtpPassword)
            };
            using (var msg = new MailMessage(options.SmtpEmail, recipient)
            {
                From = new MailAddress(options.SmtpEmail, options.SmtpName),
                Subject = subject,
                Body = body
            })
            {
                msg.IsBodyHtml = true;
                await smtpClient.SendMailAsync(msg);
            }
        }
    }
}
