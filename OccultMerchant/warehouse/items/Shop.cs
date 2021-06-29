using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Storage;
using OccultMerchant.Models;

namespace OccultMerchant.items
{
    public class Shop : Base
    {
        public List<(Potion, int)> potionStore { get; set; }

        public int Space { get; set; }

        public Shop() : base()
        {
            this.potionStore = new List<(Potion, int)>();
        }

        public Shop(int id) : base()
        {
            this.id = id;
        }

        public Shop(long id, string _name, string _description, string _source, Price _price,
            List<(Potion, int)> potionStore) : base(id, _name, _description, _source, _price)
        {
            this.potionStore = potionStore;
        }

        public static List<Shop> getAllShop(string name = "")
        {
            List<Shop> result = new List<Shop>();
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    if (name == "")
                    {
                        command.CommandText = @"SELECT * FROM 'Shop'";
                    }
                    else
                    {
                        command.CommandText = @"SELECT * FROM 'Shop' WHERE name=@name";
                        command.Parameters.AddWithValue("@name", name);
                    }

                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Shop tmp = new Shop();
                            tmp.id = reader.GetInt64(0);
                            tmp.name = reader.GetString(1);
                            tmp.description = reader.GetString(2);
                            tmp.source = reader.GetString(3);
                            tmp.price = Price.fromString(reader.GetString(4));
                            tmp.Space = reader.GetInt32(5);
                            result.Add(tmp);
                        }
                    }

                }
            }

            return result;
        }

        public void addToDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                   
                command.CommandText = @"INSERT INTO Shop(name,description,price,space,source) 
                                        VALUES (@name,@description,@price,@space,@source)";

                command.Parameters.AddWithValue("@name",this.name);
                command.Parameters.AddWithValue("@description",this.description);
                command.Parameters.AddWithValue("@source",this.source);
                command.Parameters.AddWithValue("@price",this.price.ToString());
                command.Parameters.AddWithValue("@space",this.Space.ToString());
                connection.Open();
                command.ExecuteNonQuery();
                }
            }
            
        }

        public void saveToDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                   
                    command.CommandText = @"UPDATE 'Shop' SET name=@name,description=@description,space=@space,source=@source
                                            WHERE id=@id";

                    command.Parameters.AddWithValue("@id",this.id.ToString());
                    command.Parameters.AddWithValue("@name",this.name);
                    command.Parameters.AddWithValue("@description",this.description);
                    command.Parameters.AddWithValue("@source",this.source);
                    command.Parameters.AddWithValue("@price",this.price.ToString());
                    command.Parameters.AddWithValue("@space",this.Space.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
           
        }
        
        public static void deleteToDatabase(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                   
                    command.CommandText = @"DELETE FROM Shop WHERE id=@id";
                    command.Parameters.AddWithValue("@id",id.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
           
        }
    }
}
    
    
