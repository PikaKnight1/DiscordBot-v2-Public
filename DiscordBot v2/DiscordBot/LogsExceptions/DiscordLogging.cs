using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace DiscordBot
{
    internal static class DiscordLogging
    {
        public static void CheckUpdates(DiscordClient discord)
        {
            MessageCreated(discord);
            MessageEdited(discord);
        }

        private static void MessageCreated(DiscordClient discord)
        {
            discord.MessageCreated += (discord, discordEvent) =>
            {
                Console.WriteLine(discordEvent.Message.Content);
                return Task.CompletedTask;
            };
        }

        private static void MessageEdited(DiscordClient discord)
        {
            discord.MessageUpdated += async (discord, discordEvent) =>
            {
                ulong logChannelNumber = 0000000000; //tymczasowo - miejsce na id kanału do logów

                try
                {
                    var before = discordEvent.MessageBefore.Content;
                    var after = discordEvent.Message.Content;
                    DiscordChannel logChannel = await discord.GetChannelAsync(logChannelNumber);
                    await logChannel.SendMessageAsync(before + " " + after);
                }
                catch (System.NullReferenceException)
                {
                    var after = discordEvent.Message.Content;
                    DiscordChannel logChannel = await discord.GetChannelAsync(logChannelNumber);
                    await logChannel.SendMessageAsync("Wiadomość sprzed edycji została dodana przed pojawieniem się bota na serwerze. Wiadomość po edycji: " + after);
                }
                catch (Exception ex)
                {
                    ExceptionHandling.ExceptionLogs(ex);
                }
            };
        }
    }
}
