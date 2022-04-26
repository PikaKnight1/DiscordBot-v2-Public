using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace DiscordBot
{
    static class Program
    {

        readonly static IEnumerable<string> prefixes = new List<string>() { ";" }; //prefixy

        static void Main()
        {
            MainAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            DiscordClient discord = new(new DiscordConfiguration
            {
                Token = "00000000000000000000000000000000",   // token bota
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.DirectMessageReactions
                    | DiscordIntents.DirectMessages
                    | DiscordIntents.GuildBans
                    | DiscordIntents.GuildEmojis
                    | DiscordIntents.GuildInvites
                    | DiscordIntents.GuildMembers
                    | DiscordIntents.GuildMessages
                    | DiscordIntents.Guilds
                    | DiscordIntents.GuildVoiceStates
                    | DiscordIntents.GuildWebhooks,
            });

            CommandsNextExtension commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = prefixes,
                CaseSensitive = false,
                EnableMentionPrefix = true,
                EnableDefaultHelp = false
            });

            commands.RegisterCommands<AdminCommands>();
            commands.RegisterCommands<FunCommands>();
            commands.RegisterCommands<FunCommandsAdmin>();

            await discord.ConnectAsync();
            DiscordLogging.CheckUpdates(discord);

            await Task.Delay(-1);
        }
    }
}