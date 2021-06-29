using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using warehouse.Database;

namespace warehouse.items
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

        public Weapon(long id, string _name, string _description, string _source, Price _price, Dice dmgM, string critical, WeaponsType typeWeapons, int range, string proficiency, string category) : base(id, _name, _description, _source, _price)
        {
            this.dmgM = dmgM;
            this.critical = critical;
            this.typeWeapons = typeWeapons;
            this.range = range;
            this.proficiency = proficiency;
            this.category = category;
        }

         public  static List<Shop> getAll(string name = "")
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
                        command.CommandText = @"SELECT * FROM 'Shop' WHERE name=name";
                        command.Parameters.AddWithValue("@name", $"%{name}%");
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
                   
                command.CommandText = @"INSERT INTO Weapons(name,description,dmgM,critical,range,source,proficiency,type,category,price) 
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
                   
                    command.CommandText = @"UPDATE 'Weapons' SET name=@name,description=@description,dmgM=@dmgM,critical=@critical,range=@range,source=@source,proficiency=@proficiency,type=@type,category=@category,price=@price
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
                   
                    command.CommandText = @"DELETE FROM Weapons WHERE id=@id";
                    command.Parameters.AddWithValue("@id",id.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
           
        }
    }
}