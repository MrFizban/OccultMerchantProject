using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondaryLocation.Items
{
    public class Item : IItem
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public int price { get; set; }
        [Column("type")]
        public int ItemType { get; set; }

        public Item()
        {
            
        }

        public Item(Spell spell)
        {
            this.id = spell.id;
            this.name = spell.name;
            this.description = spell.description;
            this.source = spell.source;
            this.price = spell.price;
            this.ItemType = spell.ItemType;
        }

        public Item(Potion potion)
        {
            this.id = potion.id;
            this.name = potion.name;
            this.description = potion.description;
            this.source = potion.source;
            this.price = potion.price;
            this.ItemType = potion.ItemType;
        }
    }
}