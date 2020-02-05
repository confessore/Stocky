using MailChimp.Net;
using MailChimp.Net.Interfaces;
using MailChimp.Net.Models;
using Microsoft.Extensions.Options;
using Stocky.Discord.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Stocky.Discord.Services
{
    public class MailChimpService : IMailChimpService
    {
        readonly Options.MailChimpOptions options;
        readonly IMailChimpManager manager;

        public MailChimpService(IOptions<Options.MailChimpOptions> options)
        {
            this.options = options.Value;
            Console.WriteLine(this.options.ApiKey);
            manager = new MailChimpManager(this.options.ApiKey);
        }

        public async Task<List> GetFirstListAsync()
        {
            var lists = await manager.Lists.GetAllAsync();
            return lists.FirstOrDefault();
        }

        public async Task<Campaign> GetFirstCampaignAsync()
        {
            var campaigns = await manager.Campaigns.GetAllAsync();
            return campaigns.FirstOrDefault();
        }

        public async Task<Folder> GetFirstCampaignFolder()
        {
            var listSegments = await manager.CampaignFolders.GetAllAsync();
            return listSegments.FirstOrDefault();
        }
    }
}
