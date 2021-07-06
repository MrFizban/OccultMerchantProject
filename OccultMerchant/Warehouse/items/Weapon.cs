using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Data.Sqlite;
using Warehouse.Controllers;
using Warehouse.Database;

namespace Warehouse.items
{
    public enum WeaponsType
    {
        bludgeoning,
        piercing,
        slashing        
    }
    public class Weapon : Base
    {
        
        public Dice dmgM { get; set; }                  // il danno dell arma di taglia media
        public string critical { get; set; }            // il range del critico e il moltiplicatore
        public WeaponsType typeWeapons { get; set; }    // il tipo di danno fisico dell arma
        public int range { get; set; }                  // il possibile range dell arma
        public string proficiency { get; set; }         // la proficiency richiesta per usare l'arma
        
        public string category { get; set; }

        public Weapon() : base()
        {
            this.dmgM = new Dice(1, 4);
            this.critical = "18-20/x5";
            this.typeWeapons = WeaponsType.bludgeoning;
            this.range = 9;
            this.proficiency = "Exotic";
        }

        public Weapon(long id, string _name, string _description, string _source, Price _price, Filter filter, Dice dmgM, string critical, WeaponsType typeWeapons, int range, string proficiency, string category) : base(id, _name, _description, _source, _price, filter)
        {
            this.dmgM = dmgM;
            this.critical = critical;
            this.typeWeapons = typeWeapons;
            this.range = range;
            this.proficiency = proficiency;
            this.category = category;
        }

        public  static List<Weapon> getAll(long id = 0, string name = "")
        {
            List<Weapon> result = new List<Weapon>();
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    
                    command.CommandText = @"SELECT * FROM 'Weapon'";
                    
                    if (id > -1)
                    {
                        command.CommandText += @" WHERE id=@id";
                        command.Parameters.AddWithValue("@id", id.ToString());
                    }
                    else if(name != "")
                    {
                        command.CommandText = @" WHERE name=@name";
                        command.Parameters.AddWithValue("@name", $"%{name}%");
                    } 

                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Weapon tmp = new Weapon();
                            tmp.id = reader.GetInt64(0);
                            tmp.name = reader.GetString(1);
                            tmp.description = reader.GetString(2);
                            tmp.dmgM = new Dice(reader.GetString(3));
                            tmp.critical = reader.GetString(4);
                            tmp.range = reader.GetInt32(5);
                            tmp.source = reader.GetString(6);
                            tmp.proficiency = reader.GetString(7);
                            tmp.typeWeapons = (WeaponsType) reader.GetInt32(8);
                            tmp.category = reader.GetString(9);
                            tmp.price = Price.fromString(reader.GetString(10));
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
                   
                command.CommandText = @"INSERT INTO Weapon(name,description,dmgM,critical,range,source,proficiency,type,category,price) 
                                        VALUES (@name,@description,@dmgM,@critical,@range,@source,@proficiency,@type,@category,@price)";

                command.Parameters.AddWithValue("@name",this.name);
                command.Parameters.AddWithValue("@description",this.description);
                command.Parameters.AddWithValue("@dmgM",this.source);
                command.Parameters.AddWithValue("@critical",this.price.ToString());
                command.Parameters.AddWithValue("@range",this.range.ToString());
                command.Parameters.AddWithValue("@source",this.source.ToString());
                command.Parameters.AddWithValue("@proficiency",this.proficiency.ToString());
                command.Parameters.AddWithValue("@type",this.typeWeapons.ToString());
                command.Parameters.AddWithValue("@category",this.category.ToString());
                command.Parameters.AddWithValue("@price",this.price.ToString());
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
                   
                    command.CommandText = @"UPDATE Weapon SET name=@name,description=@description,dmgM=@dmgM,critical=@critical,range=@range,source=@source,proficiency=@proficiency,type=@type,category=@category,price=@price
                                            WHERE id=@id";

                    command.Parameters.AddWithValue("@id",this.id.ToString());
                    command.Parameters.AddWithValue("@name",this.name);
                    command.Parameters.AddWithValue("@description",this.description);
                    command.Parameters.AddWithValue("@dmgM",this.source);
                    command.Parameters.AddWithValue("@critical",this.price.ToString());
                    command.Parameters.AddWithValue("@range",this.range.ToString());
                    command.Parameters.AddWithValue("@source",this.source.ToString());
                    command.Parameters.AddWithValue("@proficiency",this.proficiency.ToString());
                    command.Parameters.AddWithValue("@type",this.typeWeapons.ToString());
                    command.Parameters.AddWithValue("@category",this.category.ToString());
                    command.Parameters.AddWithValue("@price",this.price.ToString());
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
                   
                    command.CommandText = @"DELETE FROM Weapon WHERE id=@id";
                    command.Parameters.AddWithValue("@id",id.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
           
        }
    }
}