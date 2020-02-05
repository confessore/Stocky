using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Stocky.Net.Areas.Identity.IdentityHostingStartup))]
namespace Stocky.Net.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}