using Stocky.Net.Models;
using System.Threading.Tasks;

namespace Stocky.Net.Services.Interfaces
{
    public interface IDiscordService
    {
        Task<DiscordUser> GetDiscordUserByApplicationUserAsync(ApplicationUser applicationUser);
        Task<DiscordUser> GetDiscordUserByIdAsync(string id);
        Task<string> CreateNewInviteAsync();
        Task UpdateUserAsync(ApplicationUser applicationUser);
    }
}
