using System.Threading.Tasks;

namespace Stocky.Discord.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipient, string subject, string body);
    }
}
