using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using SecondaryLocation.Items;

namespace SecondaryLocation.Reposotory
{
    public class SpellRepository :  ISpellRepository
    {


        public async Task<IEnumerable<ISpell>> getAllSpell()
        {
            List<ISpell> result = new();
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM 'Item' JOIN Spell S on Item.id = S.id";

                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ISpell tmp = new Spell();
                            tmp.id = reader.GetGuid(0);
                            tmp.name = reader.GetString(1);
                            tmp.description = reader.GetString(2);
                            tmp.source = reader.GetString(3);
                            tmp.price = reader.GetInt32(4);
                            tmp.ItemType = reader.GetInt32(5);
                            tmp.range =  reader.GetInt32(7);
                            tmp.target =  reader.GetString(8);
                            tmp.duration =  reader.GetString(9);
                            tmp.savingThrow =  reader.GetString(10);
                            tmp.spellResistence =  reader.GetBoolean(11);
                            tmp.casting =  reader.GetString(12);
                            tmp.component =  reader.GetString(13);
                            tmp.school =  reader.GetString(14);
                            tmp.level =  reader.GetString(15);
                            result.Add(tmp);
                        }
                    }
                }
            }

            return result;
        }
        
        public async Task<IEnumerable<ISpell>> getAllSpellContext()
        {
            throw new NotImplementedException();
        }

        public async Task<ISpell> getSpell(Guid id)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =  @"SELECT * FROM 'Item' as it JOIN Spell S on it.id = S.id WHERE it.id = @id";
                    command.Parameters.AddWithValue("@id", id.ToString());
                    connection.Open();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Spell tmp = new Spell();
                            tmp.id = reader.GetGuid(0);
                            tmp.name = reader.GetString(1);
                            tmp.description = reader.GetString(2);
                            tmp.source = reader.GetString(3);
                            tmp.price = reader.GetInt32(4);
                            tmp.ItemType = reader.GetInt32(5);
                            tmp.range =  reader.GetInt32(7);
                            tmp.target =  reader.GetString(8);
                            tmp.duration =  reader.GetString(9);
                            tmp.savingThrow =  reader.GetString(10);
                            tmp.spellResistence =  reader.GetBoolean(11);
                            tmp.casting =  reader.GetString(12);
                            tmp.component =  reader.GetString(13);
                            tmp.school =  reader.GetString(14);
                            tmp.level =  reader.GetString(15);
                            return tmp;
                        }
                    }
                }
            }

            return null;
        }

        public async Task<ISpell> addSpell(ISpell spell)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO 'Item'(id, name, description, source, price, type) 
                                            VALUES (@id, @name, @description, @source, @price, @type)";
                    command.Parameters.AddWithValue("@id", spell.id.ToString());
                    command.Parameters.AddWithValue("@name", spell.name.ToString());
                    command.Parameters.AddWithValue("@description", spell.description.ToString());
                    command.Parameters.AddWithValue("@source", spell.source.ToString());
                    command.Parameters.AddWithValue("@price", spell.price.ToString());
                    command.Parameters.AddWithValue("@type", ((int) ItemType.spell).ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                    
                    command.CommandText = @"INSERT INTO 'Spell'(id, range, target, duration, savingThrow, spellResistence, casting, component, school, level)
                                            VALUES (@id, @range, @target, @duration, @savingThrow, @spellResistence, @casting, @component, @school, @level)";
                    command.Parameters.AddWithValue("@range", spell.range.ToString());
                    command.Parameters.AddWithValue("@target", spell.target.ToString());
                    command.Parameters.AddWithValue("@duration", spell.duration.ToString());
                    command.Parameters.AddWithValue("@savingThrow", spell.savingThrow.ToString());
                    command.Parameters.AddWithValue("@spellResistence", Convert.ToInt16(spell.spellResistence).ToString());
                    command.Parameters.AddWithValue("@casting", spell.casting.ToString());
                    command.Parameters.AddWithValue("@component", spell.component.ToString());
                    command.Parameters.AddWithValue("@school", spell.school.ToString());
                    command.Parameters.AddWithValue("@level", spell.level.ToString());
                    command.ExecuteNonQuery();
                }
            }

            return spell;
        }

        public async Task<ISpell> updateSpell(ISpell spell)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"UPDATE 'Item' SET name=@name, description=@description,source=@source, price=@price, type=@type WHERE id=@id";
                    command.Parameters.AddWithValue("@id", spell.id.ToString());
                    command.Parameters.AddWithValue("@name", spell.name.ToString());
                    command.Parameters.AddWithValue("@description", spell.description.ToString());
                    command.Parameters.AddWithValue("@source", spell.source.ToString());
                    command.Parameters.AddWithValue("@price", spell.price.ToString());
                    command.Parameters.AddWithValue("@type", spell.ItemType.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();

                    command.CommandText =
                        @"UPDATE 'Spell' SET id=@id, range=@range, target=@target, duration=@duration, savingThrow=@savingThrow, 
                                            spellResistence=@spellResistence, casting=@casting, component=@component, school=@school, level=@level
                                            WHERE id=@id;";
                                            
                    command.Parameters.AddWithValue("@range", spell.range.ToString());
                    command.Parameters.AddWithValue("@target", spell.target.ToString());
                    command.Parameters.AddWithValue("@duration", spell.duration.ToString());
                    command.Parameters.AddWithValue("@savingThrow", spell.savingThrow.ToString());
                    command.Parameters.AddWithValue("@spellResistence", spell.spellResistence.ToString());
                    command.Parameters.AddWithValue("@casting", spell.casting.ToString());
                    command.Parameters.AddWithValue("@component", spell.component.ToString());
                    command.Parameters.AddWithValue("@school", spell.school.ToString());
                    command.Parameters.AddWithValue("@level", spell.level.ToString());
                    command.ExecuteNonQuery();
                    
                }
            }

            return spell;
        }

        public async Task<bool> deleteSpell(Guid id)
        {
            using (SqliteConnection connection = Database.connection)
            {
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM 'Spell' WHERE id=@id";
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