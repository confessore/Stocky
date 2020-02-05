using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stocky.Discord.Services;
using Stocky.Discord.Services.Interfaces;
using Stocky.Discord.Services.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Stocky.Discord
{
    class Program
    {
        readonly IConfiguration configuration;
        readonly DiscordSocketClient client;
        readonly IServiceProvider services;

        Program()
        {
            configuration = BuildConfiguration();
            client = new DiscordSocketClient();
            services = ConfigureServices();
        }

        static void Main(string[] args) =>
            new Program().MainAsync().GetAwaiter().GetResult();

        async Task MainAsync()
        {
            await services.GetRequiredService<IRegistrationService>().InitializeRegistrationAsync();
            await client.LoginAsync(
                TokenType.Bot,
                Environment.GetEnvironmentVariable("StockyDiscordToken"));
            await client.StartAsync();
            await client.SetGameAsync("'>help' for commands");
            await Task.Delay(-1);
        }

        IConfiguration BuildConfiguration() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

        IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton<CommandService>()
                .AddSingleton<IRegistrationService, RegistrationService>()

                .AddSingleton<IEventService, EventService>()
                .Configure<EmailSenderOptions>(configuration.GetSection("Email"))

                .AddSingleton<IEmailService, EmailService>()
                .Configure<MailChimpOptions>(configuration.GetSection("MailChimp"))

                .AddSingleton<IMailChimpService, MailChimpService>()
                .AddSingleton<IDiscordService, DiscordService>()
                .BuildServiceProvider();
        }
    }
}
