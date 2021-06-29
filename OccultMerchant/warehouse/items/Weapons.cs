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
    public class Weapons : Base
    {
        
        public Dice dmgM { get; set; }                  // il danno dell arma di taglia media
        public string critical { get; set; }            // il range del critico e il moltiplicatore
        public WeaponsType typeWeapons { get; set; }    // il tipo di danno fisico dell arma
        public int range { get; set; }                  // il possibile range dell arma
        public string proficiency { get; set; }         // la proficiency richiesta per usare l'arma
        
        public string category { get; set; }

        public Weapons() : base()
        {
            this.dmgM = new Dice(1, 4);
            this.critical = "18-20/x5";
            this.typeWeapons = WeaponsType.bludgeoning;
            this.range = 9;
            this.proficiency = "Exotic";
        }

        public Weapons(long id, string _name, string _description, string _source, Price _price, Dice dmgM, string critical, WeaponsType typeWeapons, int range, string proficiency, string category) : base(id, _name, _description, _source, _price)
        {
            this.dmgM = dmgM;
            this.critical = critical;
            this.typeWeapons = typeWeapons;
            this.range = range;
            this.proficiency = proficiency;
            this.category = category;
        }

        public static List<Weapons> weaponsList()
        {
            var connection = DatabaseManager.getConnection();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM 'Weapons';";
            List<Weapons> list = new List<Weapons>();
               
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(Weapons.fromDatabase(reader));
                }
            }
            
            DatabaseManager.closeConnection();
            return list;
        }
        
        /// <summary>
        /// get a weapons object from reader database
        /// </summary>
        /// <param name="reader">SqliteDataReader </param>
        /// <returns></returns>
        private static Weapons fromDatabase(SqliteDataReader reader)
        {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            var description = reader.GetString(2);
            var dmgM = reader.GetString(3);
            var critical = reader.GetString(4);
            var range = reader.GetInt32(5);
            var source = reader.GetString(6);
            var proficiency = reader.GetString(7);
            var typeWeapon = reader.GetInt32(8);
            var category = reader.GetString(9);
            var price = reader.GetString(10);
            var tmp = new Weapons(id,name,description,source,new Price(price),new Dice(dmgM),
                critical,(WeaponsType) typeWeapon,range,proficiency,category);
            
            return tmp;
        }

        public void insertToDatabase()
        {
            var connection = DatabaseManager.getConnection();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
                @"INSERT INTO 'Weapons'(name,description,dmgM,critical,range,source,proficiency,type,price) 
                VALUES (@name,@description,@dmgM,@critical,@range,@source,@proficiency,@type,@price);";
            command.Parameters.AddWithValue("@name", this.name);
            command.Parameters.AddWithValue("@description", this.description);
            command.Parameters.AddWithValue("@dmgM", this.dmgM.ToString());
            command.Parameters.AddWithValue("@critical", this.critical);
            command.Parameters.AddWithValue("@range", this.range.ToString());
            command.Parameters.AddWithValue("@source", this.source);
            command.Parameters.AddWithValue("@proficiency", this.proficiency);
            command.Parameters.AddWithValue("@type", ((int)this.typeWeapons).ToString());
            command.Parameters.AddWithValue("@price", this.price.ToString());
            command.ExecuteNonQuery();
            DatabaseManager.closeConnection();

        }
        
        public void updateToDatabase()
        {
            var connection = DatabaseManager.getConnection();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
                @"UPDATE 'Weapons' 
                  SET name=@name,description=@description,dmgM=@dmgM,critical=@critical,range=@range,
                    source=@source,proficiency=@proficiency,type=@type,price=@price
                  WHERE id=@id;";
            command.Parameters.AddWithValue("@id", this.id.ToString());
            command.Parameters.AddWithValue("@name", this.name);
            command.Parameters.AddWithValue("@description", this.description);
            command.Parameters.AddWithValue("@dmgM", this.dmgM.ToString());
            command.Parameters.AddWithValue("@critical", this.critical);
            command.Parameters.AddWithValue("@range", this.range.ToString());
            command.Parameters.AddWithValue("@source", this.source);
            command.Parameters.AddWithValue("@proficiency", this.proficiency);
            command.Parameters.AddWithValue("@type", ((int)this.typeWeapons).ToString());
            command.Parameters.AddWithValue("@price", this.price.ToString());
            command.ExecuteNonQuery();
            DatabaseManager.closeConnection();

        }

        public static void deleteFromDatabase(int value)
        {
            var connection = DatabaseManager.getConnection();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM 'Weapons' WHERE id=@id";
            command.Parameters.AddWithValue("@id", value.ToString());
            command.ExecuteNonQuery();
            DatabaseManager.closeConnection();
        }
    }
}