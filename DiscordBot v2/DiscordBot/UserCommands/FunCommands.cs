using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;

namespace DiscordBot
{
    internal class FunCommands : BaseCommandModule
    {
        readonly Random random = new();
        private static string GetRandomOneFromDatabase(string commandName)
        {
            string response;

            try
            {
                response = SqlConnection.SelectRandomOne(commandName);
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionLogs(ex);
                response = "Wystąpił błąd! Jeśli będzie się to powtarzać, skontaktuj się z administratorem bota.";
            }

           return response;
        }
    
        [Command("8ball")]
        [Aliases("czy")]
        public async Task EBall(CommandContext commandContext, params string[] _)
        {
            string response = GetRandomOneFromDatabase("eball");
            await commandContext.RespondAsync(response);
        }

        [Command("8time")]
        [Aliases("kiedy")]
        public async Task ETime(CommandContext commandContext, params string[] _)
        {
            string response = GetRandomOneFromDatabase("etime");
            await commandContext.RespondAsync(response);
        }

        [Command("choose")]
        [Aliases("wybierz")]
        public async Task ChooseRandomOne(CommandContext commandContext, params string[] text)
        {
            await commandContext.RespondAsync("Wybieram: " + text[random.Next(0, text.Length)]);
        }

        [Command("rnum")]
        [Aliases("losuj")]
        public async Task RNum(CommandContext commandContext, params int [] numbers)
        {
            StringBuilder result = new("Wylosowano: ");
            int start, end, amount;

            switch (numbers.Length)
                {
                case 2:
                    start = numbers[0];
                    end = numbers[1];
                    result.Append(Randomizers.SingleRandomInt(start, end));
                    break;

                case 3:
                    start = numbers[0];
                    end = numbers[1];
                    amount = numbers[2];

                    if (amount - 1 <= end - start)
                    {
                        HashSet<int> randomizedNumbers = Randomizers.MultipleRandomInt(start, end, amount);
                        result.AppendJoin(", ", randomizedNumbers.OrderBy(x => x));
                    }
                    else
                    {
                        result.Clear();
                        result.Append("Liczba możliwych do wylosowania liczb jest mniejsza od podanego zakresu.");
                    }
                    break;

                default:
                    result.Append("Nie podano odpowiedniej liczby argumentów. Prawidłowy format komendy: losuj (punkty początkowy i końcowy) (opcjonalnie ilość liczb do wylosowania)");
                    break;
            }

            await commandContext.RespondAsync(result.ToString());
        }

        [Command("ship")]
        public async Task Ship(CommandContext commandContext, params DiscordMember [] users)
        {
            string result = users.Length switch
            {
                0 => NoMentionShip(commandContext),
                1 => MentionShip(commandContext.Member, users[0]),
                2 => MentionShip(users[0], users[1]),
                _ => "Zbyt wielu użytkowników na raz ;)",
            };
            await commandContext.RespondAsync(result);
        }
        private string NoMentionShip(CommandContext commandContext)
        {
            var allMembers = commandContext.Guild.GetAllMembersAsync().Result;
            List <DiscordMember> membersWithoutBots = new();
            foreach (var member in allMembers)
            {
                if (member.IsBot)
                {
                    membersWithoutBots.Add(member);
                    Console.WriteLine(member);
                }
            }

            int randomNumber = random.Next(0, membersWithoutBots.Count);
            string result = "Najlepiej pasuje do Ciebie: " + membersWithoutBots.ElementAt(randomNumber).DisplayName;
            return result;
        }
        private string MentionShip(DiscordMember member, DiscordMember member2) 
        {
            int randomNumber = random.Next(0, 101);
            string result = String.Format("{0} pasuje do {1} na {2}% \n", member.DisplayName, member2.DisplayName, randomNumber);
            result += SqlConnection.SelectRandomOne("ship", randomNumber);
            return result;
        }
    }
}
