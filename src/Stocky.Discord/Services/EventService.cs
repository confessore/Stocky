using Discord.Commands;
using Discord.WebSocket;
using Stocky.Discord.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Stocky.Discord.Services
{
    public class EventService : IEventService
    {
        readonly IServiceProvider services;
        readonly DiscordSocketClient client;
        readonly CommandService commandService;

        public EventService(
            IServiceProvider services,
            DiscordSocketClient client,
            CommandService commandService)
        {
            this.services = services;
            this.client = client;
            this.commandService = commandService;
            client.MessageReceived += MessageReceived;
        }

        async Task MessageReceived(SocketMessage msg)
        {
            var tmp = (SocketUserMessage)msg;
            if (tmp == null) return;
            var pos = 0;
            if (!(tmp.HasCharPrefix('>', ref pos) ||
                tmp.HasMentionPrefix(client.CurrentUser, ref pos)) ||
                tmp.Author.IsBot)
                return;
            var context = new SocketCommandContext(client, tmp);
            var result = await commandService.ExecuteAsync(context, pos, services);
            if (!result.IsSuccess)
                Console.WriteLine(result.ErrorReason);
        }
    }
}
