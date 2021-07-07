using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using SecondaryLocation.Items;
using SecondaryLocation.Reposotory;


namespace SecondaryLocation.Reposotory
{
    public class ItemRepository : IItemRepository
    {
        public async Task<IEnumerable<IItem>> getAllItem()
        {
            List<IItem> result = new ();
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM 'Item'";
                    
                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IItem tmp = new Item();
                            tmp.id = reader.GetGuid(0);
                            tmp.name = reader.GetString(1);
                            tmp.description = reader.GetString(2);
                            tmp.source = reader.GetString(3);
                            tmp.price = reader.GetInt32(4);
                            tmp.ItemType = reader.GetInt32(5);
                            result.Add(tmp);
                        }
                    }
                }
            }

            return  result;
        }

        public async Task<IItem> getItem(Guid id)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM 'Item' WHERE id=@id";
                    command.Parameters.AddWithValue("@id", id.ToString());
                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IItem tmp = new Item();
                            tmp.id = reader.GetGuid(0);
                            tmp.name = reader.GetString(1);
                            tmp.description = reader.GetString(2);
                            tmp.source = reader.GetString(3);
                            tmp.price = reader.GetInt32(4);
                            tmp.ItemType = reader.GetInt32(5);
                            return tmp;
                        }
                    }
                }
            }

            return null;
        }

        public async Task<IItem> addItem(IItem item)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO 'Item'(id, name, description, source, price, type) 
                                            VALUES (@id, @name, @description, @source, @price, @type)";
                    command.Parameters.AddWithValue("@id", item.id.ToString());
                    command.Parameters.AddWithValue("@name", item.name.ToString());
                    command.Parameters.AddWithValue("@description", item.description.ToString());
                    command.Parameters.AddWithValue("@source", item.source.ToString());
                    command.Parameters.AddWithValue("@price", item.price.ToString());
                    command.Parameters.AddWithValue("@type", item.ItemType.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return item;
        }
        
        public async Task<IItem> updateItem(IItem item)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE 'Item' SET name=@name, description=@description,source=@source, price=@price, type=@type WHERE id=@id";
                    command.Parameters.AddWithValue("@id", item.id.ToString());
                    command.Parameters.AddWithValue("@name", item.name.ToString());
                    command.Parameters.AddWithValue("@description", item.description.ToString());
                    command.Parameters.AddWithValue("@source", item.source.ToString());
                    command.Parameters.AddWithValue("@price", item.price.ToString());
                    command.Parameters.AddWithValue("@type", item.ItemType.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return item;
        }
        
        public async Task<IItem> deleteItem(IItem item)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM 'Item' WHERE id=@id";
                    command.Parameters.AddWithValue("@id", item.id.ToString());
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return item;
        }
        
    }
}