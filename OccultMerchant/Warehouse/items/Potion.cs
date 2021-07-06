using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Warehouse.Controllers;
using Warehouse.Database;
using Warehouse.items;

namespace Warehouse.items
{
    public struct SpellName
    {
        public int id { get; set; }
        public string name { get; set; }

        public SpellName(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

    public class Potion : Base
    {
        // incanteimo di riferimento
        public SpellName spellName { get; set; }

        // livello dell incantatore che ha fatto la pozione
        public int levell { get; set; }

        public Potion()
        {
        }

        public Potion(long id = 0) : base(id)
        {
            this.spellName = new SpellName(0, "");
            this.levell = 3;
        }

        public Potion(long id, string _name, string _description, string _source, Price _price, Filter filter,
            SpellName spellName, int levell) : base(id, _name, _description, _source, _price, filter)
        {
            this.spellName = spellName;
            this.levell = levell;
        }
        
        

        public static Potion getPotion(int idPotion)
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"SELECT P.*,S.name FROM 'Potion' as P JOIN Spell S on S.id = P.spell WHERE P.id=@id";

                    command.Parameters.AddWithValue("@id", idPotion.ToString());
                    
                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Potion tmp = new Potion();
                            tmp.id = reader.GetInt64(0);
                            tmp.name = reader.GetString(1);
                            tmp.description = reader.GetString(2);
                            tmp.source = reader.GetString(3);
                            tmp.price = Price.fromString(reader.GetString(4));
                            tmp.spellName = new SpellName(reader.GetInt32(5), reader.GetString(7));
                            tmp.levell = reader.GetInt32(6);
                            
                            return tmp;
                            
                        }
                    }
                }
            }

            return null;
        }

        public static List<Potion> getAll(long id = -1, string name = "")
        {
            List<Potion> result = new List<Potion>();
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT P.*,S.name FROM 'Potion' as P JOIN Spell S on S.id = P.spell";
                    if (id > -1)
                    {
                        command.CommandText += @" WHERE P.id=@id";
                        command.Parameters.AddWithValue("@id", id.ToString());
                    }
                    else if (name != "")
                    {
                        command.CommandText = @" WHERE P.name=@name";
                        command.Parameters.AddWithValue("@name", $"%{name}%");
                    }


                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Potion tmp = new Potion();
                            tmp.id = reader.GetInt64(0);
                            tmp.name = reader.GetString(1);
                            tmp.description = reader.GetString(2);
                            tmp.source = reader.GetString(3);
                            tmp.price = Price.fromString(reader.GetString(4));
                            tmp.spellName = new SpellName(reader.GetInt32(5), reader.GetString(7));
                            tmp.levell = reader.GetInt32(6);
                            result.Add(tmp);
                        }
                    }
                }
            }

            return result;
        }

        public long addToDatabase()
        {
            long result = 0;
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Potion(name,description, source, price, spell, levell) 
                                        VALUES (@name,@description,@source,@price,@spell,@levell);";

                    command.Parameters.AddWithValue("@name", this.name);
                    command.Parameters.AddWithValue("@description", this.description);
                    command.Parameters.AddWithValue("@source", this.source);
                    command.Parameters.AddWithValue("@price", this.price.ToString());
                    command.Parameters.AddWithValue("@spell", this.spellName.id.ToString());
                    command.Parameters.AddWithValue("@levell", this.levell.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT  last_insert_rowid()";
                    result = (long) command.ExecuteScalar();
                    Console.WriteLine($"id:\t{result}");
                }
            }

            this.id = result;
            return result;
        }

        public void saveToDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    if (this.filter.names.Count == 0)
                    {
                        command.CommandText =
                            @"UPDATE 'Potion' SET name=@name,description=@description,source=@source,price=@price,spell=@spell,levell=@levell
                                            WHERE id=@id";
                    }
                    else
                    {
                        command.CommandText =
                            @"UPDATE 'Potion' SET " + this.filter.setSting() + " WHERE id=@id";
                    }

                    command.Parameters.AddWithValue("@id", this.id.ToString());
                    command.Parameters.AddWithValue("@name", this.name);
                    command.Parameters.AddWithValue("@description", this.description);
                    command.Parameters.AddWithValue("@source", this.source);
                    command.Parameters.AddWithValue("@price", this.price.ToString());
                    command.Parameters.AddWithValue("@spell", this.spellName.id.ToString());
                    command.Parameters.AddWithValue("@levell", this.levell.ToString());
                    connection.Open();

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqliteException e)
                    {
                        Console.WriteLine("Error with:\t " + command.CommandText);
                        Console.WriteLine(this.filter.names.Count);
                    }
                }
            }
        }

        public static void deleteToDatabase(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    Console.WriteLine("id:\t" + id.ToString());
                    command.CommandText = @"DELETE FROM Potion WHERE id=@id";
                    command.Parameters.AddWithValue("@id", id.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}