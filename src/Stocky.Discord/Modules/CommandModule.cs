using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Stocky.Discord.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocky.Discord.Modules
{
    public class CommandModule : ModuleBase<SocketCommandContext>
    {
        readonly IServiceProvider services;
        readonly DiscordSocketClient client;
        readonly CommandService commands;
        readonly IEmailService emailSender;

        public CommandModule(
            IServiceProvider services,
            DiscordSocketClient client,
            CommandService commands,
            IEmailService emailSender)
        {
            this.services = services;
            this.client = client;
            this.commands = commands;
            this.emailSender = emailSender;
        }

        [Command("help")]
        [Summary("all: displays available commands" +
            "\n >help")]
        async Task HelpAsync()
        {
            await RemoveCommandMessageAsync();
            var embedBuilder = new EmbedBuilder();
            foreach (var command in await commands.GetExecutableCommandsAsync(Context, services))
                embedBuilder.AddField(command.Name, command.Summary ?? "no summary available");
            await ReplyAsync("here's a list of commands and their summaries: ", false, embedBuilder.Build());
        }

        [Command("invite")]
        [Summary("all: sends an invite via email" +
            "\n >help")]
        async Task InviteAsync()
        {
            await RemoveCommandMessageAsync();
            var stc = (SocketTextChannel)Context.Channel;
            var invite = await stc.CreateInviteAsync();
            Console.WriteLine(invite.Url);
            await emailSender.SendEmailAsync("steven.confessore@gmail.com", "discord invitation", invite.Url);
        }

        async Task RemoveCommandMessageAsync() =>
            await client.GetGuild(Context.Guild.Id).GetTextChannel(Context.Message.Channel.Id).DeleteMessageAsync(Context.Message);
    }
}
