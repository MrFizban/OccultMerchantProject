using System;
using System.Data;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.Sqlite;

namespace OccultMerchant.Models
{
    public static class DatabaseManager
    {
        private static SqliteConnection connection = new SqliteConnection("Data Source=../ItemsDatabase.sqlite");
        
        /// <summary>
        /// restituisce la conesione al database. La apre se è chisa;
        /// </summary>
        /// <returns></returns>
        public static SqliteConnection getConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            Console.WriteLine(connection);
            Console.WriteLine(connection.State.ToString());
            return connection;
        }

        public static void closeConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
    
}