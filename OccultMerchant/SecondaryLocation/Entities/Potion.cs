using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SecondaryLocation.Entities
{
    public class Potion : IPotion
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public int price { get; set; }
        public int ItemType { get; set; }
        public Spell spell { get; set; }
        public string level { get; set; }
        public int casterLevell { get; set; }
        public int wheight { get; set; }

        public Potion()
        {
        }

        public Potion(Item item, PotionWrapper potion)
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite("Data Source=SecondaryLocation.sqlite;").Options;
            using (var context = new ApplicationDbContext(contextOptions))
            {
                this.id = item.id;
                this.name = item.name;
                this.description = item.description;
                this.source = item.source;
                this.price = item.price;
                this.ItemType = item.ItemType;
                this.casterLevell = potion.casterLevell;
                this.wheight = potion.wheight;
                // query spell
                this.spell = new Spell(
                    // cercha la perte item
                    context.Item.Where(item => item.id == potion.idSpell).SingleOrDefault(),
                    // cerca la parte spell
                    context.Spell.Where(wrappper => wrappper.id == potion.idSpell).SingleOrDefault());
            }
        }
    }
}