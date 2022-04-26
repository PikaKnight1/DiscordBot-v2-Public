using MySqlConnector;

namespace DiscordBot
{
    static class SqlConnection
    {
        public static void CreateConnection(out MySqlConnection connection)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "192.168.100.1",     //mysql - adres serwera
                Database = "discordbot",        //database name
                UserID = "bot",                 //username
                Password = "1234"               //password
            };

                connection = new MySqlConnection(builder.ConnectionString);
        }

        public static void InsertInto(string commandName, string text)
        {
            CreateConnection(out MySqlConnection connection);

            try
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = string.Format(@"INSERT INTO {0} VALUES (@val1)", commandName);
                command.Parameters.AddWithValue("@val1", text);
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionLogs(ex);
                Console.WriteLine(ex.Message);
            }
        }

        public static void InsertInto(string commandName, string text, DateTime date)
        {
            CreateConnection(out MySqlConnection connection);

            try
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = string.Format(@"INSERT INTO {0} VALUES (@val1, @val2)", commandName);
                command.Parameters.AddWithValue("@val1", date);
                command.Parameters.AddWithValue("@val2", text);
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionLogs(ex);
            }
        }

        public static void InsertInto(string commandName, string text, int parameter)
        {
            CreateConnection(out MySqlConnection connection);

            try
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = string.Format(@"INSERT INTO {0} VALUES (@val1, @val2)", commandName);
                command.Parameters.AddWithValue("@val1", text);
                command.Parameters.AddWithValue("@val2", parameter);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionLogs(ex);
            }
        }

        public static string SelectRandomOne(string commandName)
        {
            MySqlDataReader reader;
            CreateConnection(out MySqlConnection connection);
            string result = "";

            try
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = string.Format(@"SELECT answer FROM {0} ORDER BY RAND() LIMIT 1", commandName);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionLogs(ex);
                result = "Wystąpił błąd! Jeśli będzie się to powtarzać, skontaktuj się z administratorem bota";
            }

            return result;
        }

        public static string SelectRandomOne(string commandName, int parameter)
        {
            MySqlDataReader reader;
            CreateConnection(out MySqlConnection connection);
            string result = "";

            try
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = string.Format(@"SELECT answer FROM {0} WHERE parameter <= {1} AND parameter >= {2} 
                                                      ORDER BY RAND() LIMIT 1", commandName, parameter, parameter - 10);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionLogs(ex);
                result = "Wystąpił błąd! Jeśli będzie się to powtarzać, skontaktuj się z administratorem bota";
            }

            return result;
        }
    }
}
