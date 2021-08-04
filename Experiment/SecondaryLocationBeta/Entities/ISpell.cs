using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondaryLocation.Entities
{
    public interface ISpell
    {
        [Key]
        [ForeignKey("id")]
        Guid id { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        int range { get; set; }
        string target { get; set; }
        string duration { get; set; }
        string savingThrow { get; set; }
        bool spellResistence { get; set; }
        string casting { get; set; }
        string component { get; set; }
        string school { get; set; }
        string level { get; set; }
    }
}