using MailChimp.Net.Models;
using System.Threading.Tasks;

namespace Stocky.Discord.Services.Interfaces
{
    public interface IMailChimpService
    {
        Task<List> GetFirstListAsync();
        Task<Campaign> GetFirstCampaignAsync();
        Task<Folder> GetFirstCampaignFolder();
    }
}
