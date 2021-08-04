using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SecondaryLocation.Entities
{
    public partial class Item : IItem
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public int price { get; set; }
        [Column("type")]
        public int ItemType { get; set; }

        public Spell? spell { get; set; }
        public Potion? potion { get; set; }

        public Item()
        {
        }
        
        public Item(Guid id)
        {
            this.id = id;
        }

        
    }
}