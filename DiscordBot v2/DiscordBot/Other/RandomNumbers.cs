namespace DiscordBot
{
    internal static class Randomizers
    {
        public static int SingleRandomInt(int start, int end)
        {
            Random random = new();

            int result;
            if (start <= end)
            {
                result = random.Next(start, end + 1);
            }
            else
            {
                result = random.Next(end, start + 1);
            }

            return result;
        }

        public static HashSet<int> MultipleRandomInt(int start, int end, int amount)
        {
            HashSet<int> result = new ();
            while (result.Count < amount)
            {
                result.Add(SingleRandomInt(start, end));
            }

            return result;
        }
    }
}
