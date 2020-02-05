using System.Threading.Tasks;

namespace Stocky.Net.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipient, string subject, string body);
    }
}
