using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using warehouse.Database;

namespace warehouse.items
{
    public struct CasterClass
    {
        // il livello a cui viene castato l'incantesimo
        public int spellLevel { get; set; }
        // il nome della classe del caster
        public string casterName { get; set; }

        public CasterClass(int spellLevel, string casterName)
        {
            this.spellLevel = spellLevel;
            this.casterName = casterName;
        }

        public static List<CasterClass> fromString(string str)
        {
            string[] strListraw = str.Split('|');
            List<CasterClass> result = new List<CasterClass>();
            foreach (string s in strListraw)
            {
                var tmp = s.Split(":");
                result.Add(new CasterClass(int.Parse(tmp[0].Substring(1)),tmp[1].Remove(tmp[1].Length-1)));
            }

            return result;
        }

        public override string ToString()
        {
            return $"[{this.spellLevel.ToString()}:{this.casterName}]";
        }

        public static string ListToString(List<CasterClass> list)
        {
            string restule = "";
            foreach (CasterClass casterClass in list)
            {
                restule += casterClass.ToString() + "|";
            }
          
            return  restule.Remove(1);
        }
    }

    public enum Component
    {
        Verbal = 0,
        Somatic = 1,
        Material = 2,
        Focus = 3,
        DivineFocus = 4
        
    }
    public class Spell : Base
    {
        
        
        // quali caste possono usare l'incantesimo e a che livello
        public List<CasterClass> castersPossibility { get; set; }
        // list of component
        public List<Component> componentList { get; set; }
        
        public Spell() : base()
        {
            this.castersPossibility = new List<CasterClass>()
            {
                new CasterClass(2,"Mirto")
            };
            this.componentList = new List<Component>();
        }

        public Spell(long id,string _name, string _description, string _source, Price _price, List<CasterClass> castersPossibility, List<Component> componentList) : base(id, _name, _description, _source, _price)
        {
            this.castersPossibility = castersPossibility;
            this.componentList = componentList;
        }

        public static List<Component> componentsFromString(string str)
        {
            List<Component> result = new List<Component>();
            string tmp = str.Remove(str.Length - 1);
            tmp = tmp.Substring(1);
            string[] strList = tmp.Split(',');
            if (tmp != "")
            {
                foreach (string s in strList)
                {
                    result.Add((Component) int.Parse(s));
                }
            }

            return result;
        }
        
        public static string componentstoString(List<Component> lista)
        {
            string result = "[";
            foreach (Component component in lista)
            {
                result += (int) component;
                result +=  ",";
            }

            result.Remove(result.Length - 1);
            result += "]";
            return result;
        }
        
        public static List<Spell> getAll(string name = "")
        {
            List<Spell> result = new List<Spell>();
            using (SqliteConnection connection = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    if (name == "")
                    {
                        command.CommandText = @"SELECT * FROM 'Spell'";
                    }
                    else
                    {
                        command.CommandText = @"SELECT * FROM 'Spell' WHERE name=name";
                        command.Parameters.AddWithValue("@name", $"%{name}%");
                    }

                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Spell tmp = new Spell();
                            tmp.id = reader.GetInt64(0);
                            tmp.name = reader.GetString(1);
                            tmp.description = reader.GetString(2);
                            tmp.source = reader.GetString(3);
                            tmp.price = Price.fromString(reader.GetString(4));
                            tmp.castersPossibility = CasterClass.fromString(reader.GetString(5));
                            tmp.componentList = componentsFromString(reader.GetString(6)); 
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
                   
                command.CommandText = @"INSERT INTO Spell(name,description,source,price,casterPossibility,componentList) 
                                        VALUES (@name,@description,@source,@price,@casterPossibility,@componentList)";

                command.Parameters.AddWithValue("@name",this.name);
                command.Parameters.AddWithValue("@description",this.description);
                command.Parameters.AddWithValue("@source",this.source);
                command.Parameters.AddWithValue("@price",this.price.ToString());
                command.Parameters.AddWithValue("@casterPossibility",CasterClass.ListToString(this.castersPossibility));
                command.Parameters.AddWithValue("@componentList",this.componentList.ToString());
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
                   
                    command.CommandText = @"UPDATE 'Spell' SET name=@name,description=@description,source=@source,price=@price,casterPossibility=@casterPossibility,componentList=@componentList
                                            WHERE id=@id";

                    command.Parameters.AddWithValue("@name",this.name);
                    command.Parameters.AddWithValue("@description",this.description);
                    command.Parameters.AddWithValue("@source",this.source);
                    command.Parameters.AddWithValue("@price",this.price.ToString());
                    command.Parameters.AddWithValue("@casterPossibility",CasterClass.ListToString(this.castersPossibility));
                    command.Parameters.AddWithValue("@componentList",this.componentList.ToString());
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
                   
                    command.CommandText = @"DELETE FROM 'Spell' WHERE id=@id";
                    command.Parameters.AddWithValue("@id",id.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
           
        }
    }
}