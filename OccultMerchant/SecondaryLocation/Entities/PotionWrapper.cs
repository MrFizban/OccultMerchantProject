using System;
using System.ComponentModel.DataAnnotations;

namespace SecondaryLocation.Entities
{
    public class PotionWrapper
    {
        [Key]
        public Guid id { get; set; }
        public int casterLevell { get; set; }
        public int wheight { get; set; }
        
        public Guid idSpell { get; set; }

        public PotionWrapper()
        {
        }
        public PotionWrapper(Guid id)
        {
            this.id = id;
        }
        public PotionWrapper(Potion potion)
        {
            this.id = potion.id;
            this.casterLevell = potion.casterLevell;
            this.wheight = potion.wheight;
            this.idSpell = potion.spell.id;
        }
    }
}