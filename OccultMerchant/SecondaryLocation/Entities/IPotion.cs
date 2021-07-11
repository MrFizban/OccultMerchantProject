using System;

namespace SecondaryLocation.Entities
{
    public interface IPotion
    {
        Guid id { get; set; }
        string name { get; set; }
        string description { get; set; }
        string source { get; set; }
        int price { get; set; }
        int ItemType { get; set; }
        Spell spell { get; set; }
        string level { get; set; }
        int casterLevell { get; set; }
        int wheight { get; set; }
    }
}