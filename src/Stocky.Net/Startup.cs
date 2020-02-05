using Discord.Rest;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stocky.Net.Data;
using Stocky.Net.Models;
using Stocky.Net.Services;
using Stocky.Net.Services.Interfaces;
using System;
using System.Net.Http;

namespace Stocky.Net
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sqlServer = Environment.GetEnvironmentVariable("StockySQLServer");
            var sqlUserId = Environment.GetEnvironmentVariable("StockySQLUserId");
            var sqlPassword = Environment.GetEnvironmentVariable("StockySQLPassword");
            var sqlDatabase = Environment.GetEnvironmentVariable("StockySQLDatabase");
            var connectionString = $"server={sqlServer};userid={sqlUserId};password={sqlPassword};database={sqlDatabase};";
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication()
                .AddDiscord(x =>
                {
                    x.ClientId = Environment.GetEnvironmentVariable("StockyDiscordId");
                    x.ClientSecret = Environment.GetEnvironmentVariable("StockyDiscordSecret");
                    x.Scope.Add("email");
                    x.SaveTokens = true;
                });
            services.AddSingleton(new HttpClient());
            services.AddSingleton(new DiscordRestClient());
            services.AddScoped<IDiscordService, DiscordService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddRazorPages();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
