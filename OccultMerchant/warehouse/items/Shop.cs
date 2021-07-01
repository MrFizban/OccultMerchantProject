using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using warehouse.Database;

namespace warehouse.items
{
    public struct PotionStore
    {
        public Potion potion { get; set; }
        public int quantity { get; set; }

       public PotionStore(Potion potion, int quantity)
        {
            this.potion = potion;
            this.quantity = quantity;
        }
    }
    public class Shop : Base
    {
        public List<PotionStore> potionStore { get; set; }

        public int Space { get; set; }

        public Shop() : base()
        {
            this.potionStore = new List<PotionStore>();
        }

        public Shop(int id) : base()
        {
            this.id = id;
        }

        public Shop(long id, string _name, string _description, string _source, Price _price,
            List<PotionStore> potionStore) : base(id, _name, _description, _source, _price)
        {
            this.potionStore = potionStore;
        }

        public static List<Shop> getAll(string name = "")
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
                        command.Parameters.AddWithValue("@name", $"%{name}%");
                    }

                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Shop tmp = new Shop();
                            tmp.id = reader.GetInt32(0);
                            tmp.name = reader.GetString(1);
                            tmp.description = reader.GetString(2);
                            tmp.source = reader.GetString(3);
                            tmp.price = Price.fromString(reader.GetString(4));
                            tmp.Space = reader.GetInt32(5);

                            
                            using (SqliteCommand shopPotion = connection.CreateCommand())
                            {
                                shopPotion.CommandText = @"SELECT ShopPotion.quantity, Potion.id 
                                                            FROM ShopPotion 
                                                                JOIN Potion  on Potion.id = ShopPotion.idPotion
                                                            WHERE ShopPotion.idShop=@idShop";
                                shopPotion.Parameters.AddWithValue("@idShop", tmp.id.ToString());
                                using (SqliteDataReader shopPotioReader = shopPotion.ExecuteReader())
                                {
                                    Potion tmpPotion = new Potion();
                                    int quantity = 0;
                                    try
                                    {
                                        while (shopPotioReader.Read())
                                        {
                                            tmpPotion = Potion.getAll(id: shopPotioReader.GetInt32(1))[0];
                                            quantity = shopPotioReader.GetInt32(0);
                                        }
                                    }
                                    catch (System.InvalidOperationException e)
                                    {
                                        Console.Error.WriteLine("[DB ERROR]\t" + shopPotioReader.FieldCount);
                                    }
                                     tmp.potionStore.Add(new PotionStore(tmpPotion,quantity));
                                }


                            }
                            
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
    
    
