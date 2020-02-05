using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Stocky.Net.Services.Interfaces;

namespace Stocky.Net.Pages
{
    public class IndexModel : PageModel
    {
        readonly ILogger<IndexModel> logger;
        readonly IDiscordService discordService;

        public IndexModel(
            ILogger<IndexModel> logger,
            IDiscordService discordService)
        {
            this.logger = logger;
            this.discordService = discordService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
