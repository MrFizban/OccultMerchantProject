using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OccultMerchant.items;
using OccultMerchant.Models;

namespace OccultMerchant.showcaseitems
{
    public class ShowCase: Shop
    {
        
        public List<Weapons> WeaponsList { get; set; }

        public ShowCase()
        {
            this.WeaponsList = new List<Weapons>();
        }

        public ShowCase(int id, string name, int space, bool isActive, List<Weapons> weaponsList) : base(id, name, space, isActive)
        {
            WeaponsList = weaponsList;
        }

        public static List<ShowCase> GetShowCasesList()
        {
             var connection = DatabaseManager.getConnection();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT *
                                    FROM 'Shop' as SP;";
            
            List<ShowCase> list = new List<ShowCase>();
               
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ShowCase tmp = new ShowCase();
                    tmp.id = reader.GetInt32(0);
                    tmp.name = reader.GetString(1);
                    tmp.space = reader.GetInt32(2);
                    tmp.isActive = reader.GetBoolean(3);
                    
                    list.Add(tmp);
                    SqliteCommand commandWeapons = connection.CreateCommand();
                    commandWeapons.CommandText = @"SELECT wp.* FROM 'Weapons' as wp 
                                                   JOIN ShopWeapons SW on wp.id = SW.idWeapon
                                                   join Shop S on S.id = SW.idShop
                                                   WHERE S.id = @id";
                    commandWeapons.Parameters.AddWithValue("@id", tmp.id.ToString());
                    using (SqliteDataReader weponsReader = commandWeapons.ExecuteReader())
                    {
                        while (weponsReader.Read())
                        {
                            tmp.WeaponsList.Add(Weapons.fromDatabase(weponsReader));
                        }
                        
                    }
                }
            }
            
            DatabaseManager.closeConnection();
            return list;
        }
        
        
    }
}