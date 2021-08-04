using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SecondaryLocation.Entities
{
    [Table("Potion")]
    public partial class Potion : IPotion
    {   
        [Key]
        [ForeignKey("id")]
        public Guid id { get; set; }
        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
        //public Spell spell { get; set; }
        public string level { get; set; }
        public int casterLevell { get; set; }
        public int wheight { get; set; }
        public Spell? spell { get; set; }
        
        public Potion()
        {
        }

        public Potion(Guid id)
        {
            this.id = id;
        }
    }
}