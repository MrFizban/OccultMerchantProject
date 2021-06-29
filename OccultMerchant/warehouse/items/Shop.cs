using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Storage;
using OccultMerchant.Models;

namespace OccultMerchant.items
{
    public class Shop : Base
    {
        public List<(Potion, int)> potionStore { get; set; }

        public int Space { get; set; }

        public Shop() : base()
        {
            this.potionStore = new List<(Potion, int)>();
        }

        public Shop(long id, string _name, string _description, string _source, Price _price,
            List<(Potion, int)> potionStore) : base(id, _name, _description, _source, _price)
        {
            this.potionStore = potionStore;
        }

        public static List<Shop> getAllShop()
        {
            using (SqliteConnection conn = new SqliteConnection(DatabaseManager.connectionStrin))
            {
                using (SqliteCommand command = conn.CreateCommand())
                {
                    command.CommandText = @"SELCT * FROM 'Shop'";
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Shop tmp = new Shop();
                            tmp.id = reader.GetInt64(0);
                        }
                    }

                }
            }

            return new List<Shop>();
        }
    }
}
    
    
