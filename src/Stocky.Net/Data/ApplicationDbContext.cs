using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stocky.Net.Models;

namespace Stocky.Net.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ApplicationSubscription> ApplicationSubscription { get; set; }
        public DbSet<DiscordUser> DiscordUser { get; set; }
    }
}
