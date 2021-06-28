using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.Sqlite;
using OccultMerchant.Models;

namespace OccultMerchant.items
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        

        public Item()
        {
            this.id = 0;
            this.name = "";
            this.quantity = 0;
        }

        public Item(int id, string name, int quantity)
        {
            this.id = id;
            this.name = name;
            this.quantity = quantity;
        }
    }
    public class Shop
    {
        public int id { get; set;}
        public string name { get; set;}
        public int space { get; set;}
        public bool isActive { get; set;}
        public List<Item> weaponsItems { get; set; }

        public Shop()
        {
            this.id = 0;
            this.name = "";
            this.space = 0;
            this.isActive = false;
            this.weaponsItems = new List<Item>();
        }

        public Shop(int id, string name, int space, bool isActive)
        {
            this.id = id;
            this.name = name;
            this.space = space;
            this.isActive = isActive;
            this.weaponsItems = new List<Item>();
        }
        
        
        public static List<Shop> weaponsList()
        {
            var connection = DatabaseManager.getConnection();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM 'Shop';";
            List<Shop> list = new List<Shop>();
               
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(Shop.fromReader(reader));
                }
            }
            
            DatabaseManager.closeConnection();
            return list;
        }
        
        private static Shop fromReader(SqliteDataReader reader)
        {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            var space = reader.GetInt32(2);
            var isActive = Convert.ToBoolean(reader.GetInt32(3));
            var tmp = new Shop(id, name, space, isActive);
            tmp.getAllItems();
            return tmp;
        }

        private void getAllItems()
        {
            var connection = DatabaseManager.getConnection();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                                    SELECT sp.idShop,sp.idWeapon,sp.quantity,w.name
                                    FROM 'ShopWeapons' as sp
                                    JOIN  Weapons w on sp.idWeapon = w.id
                                    WHERE idShop=@id";
            command.Parameters.AddWithValue("@id", this.id.ToString());
            List<Item> list = new List<Item>();
            
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var idWeapons = reader.GetInt32(1);
                    var quantity = reader.GetInt32(2);
                    var name = reader.GetString(3);
                    list.Add(new Item(idWeapons,name,quantity));
                }
            }

            this.weaponsItems = list;


        }

        public void insertToDatabase()
        {
            var connection = DatabaseManager.getConnection();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
                @"INSERT INTO 'Shop'(name,space,isActive) 
                VALUES (@name,@space,@isAcvtive); SELECT last_insert_rowid()";
            command.Parameters.AddWithValue("@name", this.name);
            command.Parameters.AddWithValue("@space", this.space.ToString());
            command.Parameters.AddWithValue("@isAcvtive", Convert.ToInt32( this.isActive).ToString());
            var index = (long) command.ExecuteScalar();
            Console.WriteLine("get index: \t" + index);
            this.inserItemsToDatabase(index);
            DatabaseManager.closeConnection();
            

        }
        
        public void inserItemsToDatabase(long idShop)
        {
            Console.WriteLine(this.weaponsItems.Count);
            var connection = DatabaseManager.getConnection();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
                @"INSERT OR REPLACE INTO 'ShopWeapons'(idShop,idWeapon,quantity) 
                VALUES (@idShop,@idWeapon,@quantity);";
            
            command.Parameters.Add("@idShop", SqliteType.Integer);
            command.Parameters.Add("@idWeapon", SqliteType.Integer);
            command.Parameters.Add("@quantity",SqliteType.Integer);

            foreach (Item item in this.weaponsItems)
            {
                command.Parameters["@idShop"].Value = idShop;
                command.Parameters["@idWeapon"].Value = item.id;
                command.Parameters["@quantity"].Value = item.quantity;
                command.ExecuteNonQuery();    
            }
            
            
        }
        
        public void updateToDatabase()
        {
            Console.WriteLine("update:\t" + this.id +"." + this.name );
            var connection = DatabaseManager.getConnection();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
                @"UPDATE 'Shop' 
                  SET name=@name,space=@space,isActive=@isActive
                  WHERE id=@id;";
            command.Parameters.AddWithValue("@name", this.name);
            command.Parameters.AddWithValue("@space", this.space.ToString());
            command.Parameters.AddWithValue("@isActive", Convert.ToInt32( this.isActive).ToString());
            command.Parameters.AddWithValue("@id", this.id.ToString());
            command.ExecuteNonQuery();
            this.updateItemsToDatabse();
            DatabaseManager.closeConnection();

        }

        private void updateItemsToDatabse()
        {
            var connection = DatabaseManager.getConnection();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
                @"UPDATE 'ShopWeapons' 
                  SET quantity=@quantity
                  WHERE idShop=@idShop and idWeapon=@idWeapon;";
            command.Parameters.Add("@idShop", SqliteType.Integer);
            command.Parameters.Add("@idWeapon", SqliteType.Integer);
            command.Parameters.Add("@quantity", SqliteType.Integer);
            
            foreach (Item item in this.weaponsItems)
            {
                command.Parameters["@idShop"].Value = this.id;
                command.Parameters["@idWeapon"].Value = item.id;
                command.Parameters["@quantity"].Value = item.quantity;
                command.ExecuteNonQuery();
                Console.WriteLine(item.id);
            }

        }

        public static void deleteShopFromDatabase(int value)
        {
            var connection = DatabaseManager.getConnection();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM 'Shop' WHERE id=@id;";
            command.Parameters.Add("@id", SqliteType.Integer);
            command.Parameters["@id"].Value = value;
            command.ExecuteNonQuery();
            
            command.CommandText = @"DELETE FROM 'ShopWeapons' WHERE idShop=@id;";
            command.Parameters["@id"].Value = value;
            command.ExecuteNonQuery();
            
            DatabaseManager.closeConnection();
        }
        
        
        
        public static void deleteItemFromDatabase(int idShop, int idWeapon)
        {
            var connection = DatabaseManager.getConnection();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM 'ShopWeapons' WHERE idShop=@idShop and idWeapon=@idWeapon";
            command.Parameters.AddWithValue("@idShop", idShop.ToString());
            command.Parameters.AddWithValue("@idWeapon", idWeapon.ToString());
            command.ExecuteNonQuery();
            Console.WriteLine("delete item");
            DatabaseManager.closeConnection();
        }

    }
}