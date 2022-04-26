using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DiscordBot
{
    internal class FunCommandsAdmin : BaseCommandModule
    {
        private async static Task AddToDatabase(CommandContext commandContext, string commandName, string [] text)
        {
            string connectedText = string.Join(" ", text);

            try
            {
                SqlConnection.InsertInto(commandName, connectedText);
                await commandContext.RespondAsync("Dodano pomyślnie!");
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionLogs(ex);
                await commandContext.RespondAsync("Wystąpił błąd! Jeśli będzie się powtarzać, skontaktuj się z administratorem bota!");
            }

        }
        private async static Task AddToDatabase(CommandContext commandContext, string commandName, string [] text, int parameter)
        {
            string connectedText = string.Join(" ", text);
           
            try
            {
                SqlConnection.InsertInto(commandName, connectedText, parameter);
                await commandContext.RespondAsync("Dodano pomyślnie!");
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionLogs(ex);
                await commandContext.RespondAsync("Wystąpił błąd! Jeśli będzie się powtarzać, skontaktuj się z administratorem bota!");
            }
        }

        [Command("add8Ball")]
        [RequireUserPermissions(DSharpPlus.Permissions.KickMembers)]
        public async Task EBallAdd(CommandContext commandContext, params string[] text)
        {
            await AddToDatabase(commandContext, "eball", text);
        }

        [Command("add8Time")]
        [RequireUserPermissions(DSharpPlus.Permissions.KickMembers)]
        public async Task ETimeAdd(CommandContext commandContext, params string[] text)
        {
            await AddToDatabase(commandContext, "etime", text);
        }

        [Command("addShip")]
        [RequireUserPermissions(DSharpPlus.Permissions.KickMembers)]
        public async Task AddShip(CommandContext commandContext, int percentage, params string[] text)
        {
            await AddToDatabase(commandContext, "ship", text, percentage);
        }

    }
}
