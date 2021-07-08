using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SecondaryLocation.Items;

namespace SecondaryLocation.Reposotory
{
    public class PotionRepository: IPotionReposotory
    {
         public async Task<IEnumerable<IPotion>> getAllPotion()
        {
            List<IPotion> result = new();
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =@"SELECT I.*,P.*,I2.*,S.* FROM 'Item' as I JOIN Potion P on I.id = P.id JOIN Item as I2 on I2.id = P.idSpell JOIN Spell as S on S.id = P.idSpell ;";

                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                    
                            
                            Potion tmp = new Potion();
                            // item table
                            tmp.id = reader.GetGuid(0);
                            tmp.name = reader.GetString(1);
                            tmp.description = reader.GetString(2);
                            tmp.source = reader.GetString(3);
                            tmp.price = reader.GetInt32(4);
                            tmp.ItemType = reader.GetInt32(5);
                            // potion talbe
                            tmp.casterLevel = reader.GetInt32(7);
                            tmp.wheight = reader.GetInt32(8);
                            // spell table (aka imte+spell)
                            tmp.spell = new();
                            tmp.spell.id = reader.GetGuid(10);
                            tmp.spell.name = reader.GetString(11);
                            tmp.spell.description = reader.GetString(12);
                            tmp.spell.source = reader.GetString(13);
                            tmp.spell.price = reader.GetInt32(14);
                            tmp.spell.ItemType = reader.GetInt32(15);
                            tmp.spell.range =  reader.GetInt32(16);
                            tmp.spell.target =  reader.GetString(18);
                            tmp.spell.duration =  reader.GetString(19);
                            tmp.spell.savingThrow =  reader.GetString(20);
                            tmp.spell.spellResistence =  reader.GetBoolean(21);
                            tmp.spell.casting =  reader.GetString(22);
                            tmp.spell.component =  reader.GetString(23);
                            tmp.spell.school =  reader.GetString(24);
                            tmp.spell.level =  reader.GetString(25);
                            result.Add(tmp);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<IPotion> getPotion(Guid id)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =  @"SELECT I.*,P.*,I2.*,S.* FROM 'Item' as I JOIN Potion P on I.id = P.id JOIN Item as I2 on I2.id = P.idSpell JOIN Spell as S on S.id = P.idSpell WHERE I.id=@id ;";
                    command.Parameters.AddWithValue("@id", id.ToString());
                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IPotion tmp = new Potion();
                            tmp.id = reader.GetGuid(0);
                            tmp.name = reader.GetString(1);
                            tmp.description = reader.GetString(2);
                            tmp.source = reader.GetString(3);
                            tmp.price = reader.GetInt32(4);
                            tmp.ItemType = reader.GetInt32(5);
                            // potion talbe
                            tmp.casterLevel = reader.GetInt32(7);
                            tmp.wheight = reader.GetInt32(8);
                            // spell table (aka imte+spell)
                            tmp.spell = new();
                            tmp.spell.id = reader.GetGuid(10);
                            tmp.spell.name = reader.GetString(11);
                            tmp.spell.description = reader.GetString(12);
                            tmp.spell.source = reader.GetString(13);
                            tmp.spell.price = reader.GetInt32(14);
                            tmp.spell.ItemType = reader.GetInt32(15);
                            tmp.spell.range =  reader.GetInt32(16);
                            tmp.spell.target =  reader.GetString(18);
                            tmp.spell.duration =  reader.GetString(19);
                            tmp.spell.savingThrow =  reader.GetString(20);
                            tmp.spell.spellResistence =  reader.GetBoolean(21);
                            tmp.spell.casting =  reader.GetString(22);
                            tmp.spell.component =  reader.GetString(23);
                            tmp.spell.school =  reader.GetString(24);
                            tmp.spell.level =  reader.GetString(25);
                            return tmp;
                        }
                    }
                }
            }

            return null;
        }

        public async Task<IPotion> addPotion(IPotion potion)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO 'Item'(id, name, description, source, price, type) 
                                            VALUES (@id, @name, @description, @source, @price, @type)";
                    command.Parameters.AddWithValue("@id", potion.id.ToString());
                    command.Parameters.AddWithValue("@name", potion.name.ToString());
                    command.Parameters.AddWithValue("@description", potion.description.ToString());
                    command.Parameters.AddWithValue("@source", potion.source.ToString());
                    command.Parameters.AddWithValue("@price", potion.price.ToString());
                    command.Parameters.AddWithValue("@type", ((int) ItemType.potion).ToString());
                    connection.Open();
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO 'Potion'(id, casterLevell, wheight, idSpell)
                                            VALUES (@id, @casterLevell, @wheight, @idSpell);";
                    command.Parameters.AddWithValue("@casterLevell", potion.casterLevel.ToString());
                    command.Parameters.AddWithValue("@wheight", potion.wheight.ToString());
                    command.Parameters.AddWithValue("@idSpell", potion.spell.id.ToString());

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Microsoft.Data.Sqlite.SqliteException e)
                    {
                        if (e.SqliteErrorCode == 19)
                        {
                            return new Potion(){name = "FOREIGN KEY constraint failed" };
                        }
                    }
                }
            }

            return potion;
        }

        public async Task<IPotion> updatePotion(IPotion potion)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"UPDATE 'Item' SET name=@name, description=@description,source=@source, price=@price, type=@type WHERE id=@id";
                    command.Parameters.AddWithValue("@id", potion.id.ToString());
                    command.Parameters.AddWithValue("@name", potion.name.ToString());
                    command.Parameters.AddWithValue("@description", potion.description.ToString());
                    command.Parameters.AddWithValue("@source", potion.source.ToString());
                    command.Parameters.AddWithValue("@price", potion.price.ToString());
                    command.Parameters.AddWithValue("@type", potion.ItemType.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();

                   
                    
                    command.CommandText = @"UPDATE 'Potion' SET  casterLevell=@casterLevell, wheight=@wheight, idSpell=@idSpell
                                            WHERE id=@id;";
                    command.Parameters.AddWithValue("@casterLevell", potion.casterLevel.ToString());
                    command.Parameters.AddWithValue("@wheight", potion.wheight.ToString());
                    command.Parameters.AddWithValue("@idSpell", potion.spell.id.ToString());
                    command.ExecuteNonQuery();
                    
                }
            }

            return potion;
        }

        public async Task<bool> deletePotion(Guid id)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM 'Potion' WHERE id=@id";
                    command.Parameters.AddWithValue("@id", id.ToString());
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                    
                    command.CommandText = @"DELETE FROM 'Item' WHERE id=@id";
        
                    command.ExecuteNonQuery();
                   
                }
            }

            return true;
        }
    }
}