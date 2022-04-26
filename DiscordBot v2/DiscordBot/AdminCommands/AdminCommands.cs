using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;

namespace DiscordBot
{
    internal class AdminCommands : BaseCommandModule
    {
        [Command("kick")]
        [Hidden]
        [RequirePermissions(DSharpPlus.Permissions.BanMembers)]

        public async Task Kick(CommandContext commandContext, DiscordMember member, params string[] reasonArray)
        {
            string kickReason = String.Join(" ", reasonArray);
            await commandContext.RespondAsync("Pomyślnie wyrzucono: " + member.Username);
            await member.RemoveAsync(kickReason);
        }

        [Command("ban")]
        [Hidden]
        [RequirePermissions(DSharpPlus.Permissions.BanMembers)]
        public async Task Ban(CommandContext commandContext, DiscordMember member, params string[] reasonArray)
        {
            bool parseInt = int.TryParse(reasonArray[0], out int messageDeleteTime);

            if (parseInt)
            {
                reasonArray[0] = "";
            }
            else
            {
                messageDeleteTime = 0;
            }

            string banReason = String.Join(" ", reasonArray);

            try
            {
                await member.BanAsync(messageDeleteTime, banReason);
                await commandContext.RespondAsync("Pomyślnie zbanowano: " + member.Username);
            }
            catch (Exception)
            {
                await commandContext.RespondAsync("Nie udało się zbanować użytkownika - prawdopodobna przyczyna: użytkownik posiada rangę wyższą niż bot.");
            }
        }
    }
}