namespace DiscordBot
{
    static internal class ExceptionHandling
    {
        public static void ExceptionLogs (Exception ex)
        {
            try
            {
                SqlConnection.InsertInto("errorlog", ex.Message, DateTime.Now);
            }
            catch (Exception)
            {
                Console.WriteLine("Database error - can't upload exception info to database");
            }
        }
    }
}
