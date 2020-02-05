using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stocky.Net.Models;
using System.Threading.Tasks;

namespace Stocky.Net.Areas.Identity.Pages.Account.Manage
{
    public class PurchaseModel : PageModel
    {
        readonly UserManager<ApplicationUser> userManager;

        public PurchaseModel(
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            ApplicationUser = user;
            return Page();
        }
    }
}
