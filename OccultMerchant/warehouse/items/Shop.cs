using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;
using warehouse.Controllers;
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
        public List<PotionStore> potionReserv { get; set; }

        public int space { get; set; }
        public bool isActive { get; set; }
        public Shop() : base()
        {
            this.potionReserv = new List<PotionStore>();
        }

        public Shop(int id) : base()
        {
            this.id = id;
        }

        public Shop(long id, string _name, string _description, string _source, Price _price, Filter filter, List<PotionStore> potionReserv, int space, bool isActive) : base(id, _name, _description, _source, _price, filter)
        {
            this.potionReserv = potionReserv;
            this.space = space;
            this.isActive = isActive;
        }

        public static List<Shop> getAll(long id = -1, string name = "")
        {
            List<Shop> result = new List<Shop>();
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM 'Shop'";

                    if (id > -1)
                    {
                        command.CommandText += @" WHERE id=@id";
                        command.Parameters.AddWithValue("@id", id.ToString());
                    }
                    else if (name != "")
                    {
                        command.CommandText = @" WHERE name=@name";
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
                            tmp.space = reader.GetInt32(5);
                            tmp.isActive = reader.GetBoolean(6);


                            using (SqliteCommand shopPotion = connection.CreateCommand())
                            {
                                shopPotion.CommandText = @"SELECT Potion.id ,ShopPotion.quantity 
                                                            FROM ShopPotion 
                                                                JOIN Potion  on Potion.id = ShopPotion.idPotion
                                                            WHERE ShopPotion.idShop=@idShop";
                                shopPotion.Parameters.AddWithValue("@idShop", tmp.id.ToString());
                                using (SqliteDataReader shopPotioReader = shopPotion.ExecuteReader())
                                {
                                  
                                        while (shopPotioReader.Read())
                                        {

                                            try
                                            {
                                                Console.WriteLine("potion:\t" + shopPotioReader.GetInt32(0) + "\t" +
                                                                  "shop: \t" + shopPotion.Parameters["@idShop"].Value + "\t" +
                                                                  "quantiyt:\t" + shopPotioReader.GetInt32(1));
                                                Potion tmpPotion = new Potion();
                                                
                                                tmpPotion = Potion.getPotion(shopPotioReader.GetInt32(0));
                                                if (tmpPotion == null)
                                                {
                                                    tmpPotion = Potion.getPotion(0);
                                                    Console.WriteLine("db: is null");
                                                }
                                                
                                                if (tmpPotion == null)
                                                {
                                                    tmpPotion = new Potion(0);
                                                    Console.WriteLine("placeholder: is null");
                                                }
                                                int quantity = shopPotioReader.GetInt32(1);

                                                tmp.potionReserv.Add(new PotionStore(tmpPotion, quantity));
                                            }
                                            catch (System.ArgumentOutOfRangeException)
                                            {
                                                Console.WriteLine("ho un problema");
                                                Console.WriteLine("db data:\t" + shopPotioReader.GetInt32(0) + "\t" +
                                                                  shopPotion.Parameters["@idShop"].Value + "\t" +
                                                                  shopPotioReader.GetInt32(1));
                                                Console.WriteLine("tmp:\t" + tmp.potionReserv[tmp.potionReserv.Count-1].potion.id.ToString() 
                                                                             +"\t" + tmp.potionReserv[tmp.potionReserv.Count-1].potion.name.ToString() 
                                                                             +"\t" + tmp.potionReserv[tmp.potionReserv.Count-1].quantity );
                                            }
                                        


                                       
                                    }
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

                    command.Parameters.AddWithValue("@name", this.name);
                    command.Parameters.AddWithValue("@description", this.description);
                    command.Parameters.AddWithValue("@source", this.source);
                    command.Parameters.AddWithValue("@price", this.price.ToString());
                    command.Parameters.AddWithValue("@space", this.space.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT  last_insert_rowid()";
                    var result = (long) command.ExecuteScalar();
                    this.id = result;
                    Console.WriteLine($"id:\t{result.ToString()}");
                    
                }
            }
        }

        public void addPotions()
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO ShopPotion(idShop, idPotion,quantity) 
                                        VALUES (@idShop,@idPotion,@quantity)";

                    command.Parameters.Add("@idShop", SqliteType.Integer);
                    command.Parameters.Add("@idPotion",SqliteType.Integer);
                    command.Parameters.Add("@quantity",SqliteType.Integer);

                    connection.Open();
                    foreach (PotionStore potionStore in this.potionReserv)
                    {
                        command.Parameters["@idShop"].Value = this.id.ToString();
                        command.Parameters["@idPotion"].Value = potionStore.potion.id.ToString();
                        command.Parameters["@quantity"].Value = potionStore.quantity.ToString();
                        try
                        {
                           
                            command.ExecuteNonQuery();
                        }
                        catch (SqliteException e)
                        {
                            if (e.SqliteErrorCode == 19)
                            {
                                Console.WriteLine("[ERROR] sql UNIQUE constraint");
                                Console.WriteLine(e.Message);
                                Console.WriteLine("idShop:\t" + command.Parameters["@idShop"].Value +
                                                  "\tidPotion:\t" + command.Parameters["@idPotion"].Value +
                                                  "\tquantity:\t" + command.Parameters["@quantity"].Value);
                                command.CommandText = @"UPDATE ShopPotion 
                                        SET quantity=@quantity
                                        WHERE idShop=@idShop and idPotion=@idPotion";
                                command.ExecuteNonQuery();
                            }
                            else
                            {
                                Console.WriteLine("[Error] some else");
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                   
                }
            }
        }

        public void saveToDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"UPDATE 'Shop' SET name=@name,description=@description,space=@space,source=@source
                                            WHERE id=@id";

                    command.Parameters.AddWithValue("@id", this.id.ToString());
                    command.Parameters.AddWithValue("@name", this.name);
                    command.Parameters.AddWithValue("@description", this.description);
                    command.Parameters.AddWithValue("@source", this.source);
                    command.Parameters.AddWithValue("@price", this.price.ToString());
                    command.Parameters.AddWithValue("@space", this.space.ToString());
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
                    command.Parameters.AddWithValue("@id", id.ToString());
                    Console.WriteLine(command.Parameters["@id"].Value);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void deletePotioFromStock(int idShop, int idPotion)
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM ShopPotion 
                                            WHERE idPotion=@idPotion AND idShop=@idShop";
                    command.Parameters.AddWithValue("@idPotion", idPotion.ToString());
                    command.Parameters.AddWithValue("@idShop", idShop.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}