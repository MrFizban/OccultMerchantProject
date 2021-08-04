using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondaryLocation.Entities
{
    [Table("Spell")]
    public partial class Spell : ISpell
    {
        [Key]
        [ForeignKey("id")]
        public Guid id { get; set; }
        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
        public int range { get; set; }
        public string target { get; set; }
        public string duration { get; set; }
        public string savingThrow { get; set; }
        
        public bool spellResistence { get; set; }
        [Column("casting")]
        public string casting { get; set; }
        public string component { get; set; }
        public string school { get; set; }
        public string level { get; set; }

        public Spell()
        {
        }

        public Spell(Guid id)
        {
            this.id = id;
        }
    }
}