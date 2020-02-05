using Stocky.Net.Services.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Stocky.Net.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string recipient, string subject, string body) =>
            await ExecuteAsync(recipient, subject, body);

        async Task ExecuteAsync(string recipient, string subject, string body)
        {
            var smtpClient = new SmtpClient
            {
                Host = Environment.GetEnvironmentVariable("StockySMTPHost"),
                Port = Convert.ToInt32(Environment.GetEnvironmentVariable("StockySMTPPort")),
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    Environment.GetEnvironmentVariable("StockySMTPUserId"),
                    Environment.GetEnvironmentVariable("StockySMTPPassword"))
            };
            using var msg = new MailMessage(
                Environment.GetEnvironmentVariable("StockySMTPEmail"), recipient)
            {
                From = new MailAddress(
                    Environment.GetEnvironmentVariable("StockySMTPEmail"),
                    Environment.GetEnvironmentVariable("StockySMTPName")),
                Subject = subject,
                Body = body
            };
            msg.IsBodyHtml = true;
            await smtpClient.SendMailAsync(msg);
        }
    }
}
