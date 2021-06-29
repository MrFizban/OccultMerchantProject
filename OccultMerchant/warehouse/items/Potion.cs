using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using OccultMerchant.Models;

namespace OccultMerchant.items
{
    public class Potion : Spell
    {
        
        // incanteimo di riferimento
        public (int,string) spellName { get; set; }
        // livello dell incantatore che ha fatto la pozione
        public int levell { get; set; }

        public Potion() : base()
        {
            this.spellName = (0,"");
            this.levell = 3;
        }

        public Potion(long id, string _name, string _description, 
            string _source, Price _price, List<CasterClass> castersPossibility, (int,string) spellName, int levell, List<Component> componentList) 
            : base(id, _name, _description, _source, _price, castersPossibility,componentList)
        {
            this.spellName = spellName;
            this.levell = levell;
        }

         public static List<Potion> getAllPotion(string name = "")
        {
            List<Potion> result = new List<Potion>();
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    if (name == "")
                    {
                        command.CommandText = @"SELECT P.*,S.name FROM 'Potion' as P JOIN Spell S on S.id = P.spell";
                    }
                    else
                    {
                        command.CommandText = @"SELECT P.*,S.name FROM 'Potion' as P JOIN Spell S on S.id = P.spell WHERE name=@name";
                        command.Parameters.AddWithValue("@name", name);
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
                            tmp.spellName = (reader.GetInt32(5),reader.GetString(7));
                            tmp.levell = reader.GetInt32(6);
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