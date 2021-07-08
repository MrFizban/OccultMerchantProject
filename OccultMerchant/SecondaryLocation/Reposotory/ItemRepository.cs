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
        
        private IItem passToObject(SqliteDataReader reader){
            IItem tmp = new Item();
            tmp.id = reader.GetGuid(0);
            tmp.name = reader.GetString(1);
            tmp.description = reader.GetString(2);
            tmp.source = reader.GetString(3);
            tmp.price = reader.GetInt32(4);
            tmp.ItemType = reader.GetInt32(5);
            return tmp;
        }
        
        
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
                            result.Add(this.passToObject(reader));
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
                            return this.passToObject(reader);
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
        
        public async Task<bool>  deleteItem(Guid id)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM 'Item' WHERE id=@id";
                    command.Parameters.AddWithValue("@id", id.ToString());
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return true;
        }


        // FIND FUNCTION

        public async Task<HashSet<IItem>> find(Filter filter)
        {
            HashSet<IItem> items = new HashSet<IItem>();
            Console.WriteLine(filter);
            if (filter.name != "")
            {
                Console.WriteLine("find by name");
                this.findByName(ref items, filter.name);
            }
            
            if (filter.description != null)
            {
                Console.WriteLine("find by desc");
                this.findByDescription(ref items, filter.description);
            }
            
            if (filter.source != null)
            {
                this.findBySource(ref items, filter.source);
            }
            
            if (filter.price.Item1 != null && filter.price.Item2 != null )
            {
                this.findByPrice(ref items,filter.price.Item1 ,filter.price.Item2);
            }
            
            if (filter.itemType != null)
            {
                this.findBytype(ref items, filter.itemType);
            }


            return items;
        }
        
        private void findByName(ref HashSet<IItem> items, string name)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM 'Item' WHERE name LIKE @name;";
                    command.Parameters.AddWithValue("@name", $"@{name}@");
                    
                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(this.passToObject(reader));
                        }
                    }
                }
            }
        }
        
        private void findByDescription(ref HashSet<IItem> items, string description)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM 'Item' WHERE description=@description";
                    command.Parameters.AddWithValue("@description", description);
                    
                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(this.passToObject(reader));
                        }
                    }
                }
            }
        }
        
        private void findBySource(ref HashSet<IItem> items, string source)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM 'Item' WHERE source=@source";
                    command.Parameters.AddWithValue("@source", source);
                    
                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(this.passToObject(reader));
                        }
                    }
                }
            }
        }
        
        private void findByPrice(ref HashSet<IItem> items, char op, int price)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM 'Item'  WHERE (price>@price and @op='>') or 
                                                                        (price=@price and @op='=') or
                                                                        (price<@price and @op='<') ";
                    command.Parameters.AddWithValue("@op", op.ToString());
                    command.Parameters.AddWithValue("@price", price.ToString());
                    
                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(this.passToObject(reader));
                        }
                    }
                }
            }
        }
        
        private void findBytype(ref HashSet<IItem> items, ItemType itemType)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM 'Item' WHERE type=@type";
                    command.Parameters.AddWithValue("@type", ((int) itemType).ToString());
                    
                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(this.passToObject(reader));
                        }
                    }
                }
            }
        }
        
    }
}