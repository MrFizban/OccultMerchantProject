using Microsoft.Data.Sqlite;

namespace OccultMerchant.items
{
    public enum WeaponsType
    {
        bludgeoning,
        piercing,
        slashing        
    }
    public class Weapons : Base
    {
        // il danno dell arma di taglia media
        public Dice dmgM { get; set; }
        // il range del critico e il moltiplicatore
        public string critical { get; set; }
        // il tipo di danno fisico dell arma
        public WeaponsType type { get; set; }
        // il possibile range dell arma
        public int range { get; set; }
        // la proficiency richiesta per usare l'arma 
        public string proficiency { get; set; }

        public Weapons() : base()
        {
            this.dmgM = new Dice(1, 4);
            this.critical = "18-20/x5";
            this.type = WeaponsType.bludgeoning;
            this.range = 9;
            this.proficiency = "Exotic";
        }

        public Weapons(string _name, string _description, string _source, Price _price, Dice dmgM, string critical, 
            WeaponsType type, int range, string proficiency) : base(_name, _description, _source, _price)
        {
            this.dmgM = dmgM;
            this.critical = critical;
            this.type = type;
            this.range = range;
            this.proficiency = proficiency;
        }

        
        
        /// <summary>
        /// get a weapons object from reader database
        /// </summary>
        /// <param name="reader">SqliteDataReader </param>
        /// <returns></returns>
        public static Weapons fromDatabase(SqliteDataReader reader)
        {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            var description = reader.GetString(2);
            var dmgM = reader.GetString(3);
            var critical = reader.GetString(4);
            var range = reader.GetInt32(5);
            var source = reader.GetString(6);
            var proficiency = reader.GetString(7);
            return new Weapons(name,description,source,new Price(),new Dice(dmgM),
                critical,WeaponsType.bludgeoning,range,proficiency);
        }
    }
}