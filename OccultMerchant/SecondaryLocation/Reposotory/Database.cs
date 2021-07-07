using Microsoft.Data.Sqlite;

namespace SecondaryLocation.Reposotory
{
    public class Database
    {
        private static string connectionStrin = "Data Source=SecondaryLocation.sqlite";
        public static SqliteConnection connection = new SqliteConnection(connectionStrin);
    }
}