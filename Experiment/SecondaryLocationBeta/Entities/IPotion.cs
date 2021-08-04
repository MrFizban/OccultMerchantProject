using System;

namespace SecondaryLocation.Entities
{
    public interface IPotion
    {
        Guid id { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        Spell spell { get; set; }
        string level { get; set; }
        int casterLevell { get; set; }
        int wheight { get; set; }
    }
}