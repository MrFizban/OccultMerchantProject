using System;

namespace SecondaryLocation.Items
{
    public interface IPotion 
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public int price { get; set; }
        public int ItemType { get; set; }
        public Spell spell { get; set; }
        public int casterLevel { get; set; }
        public int wheight { get; set; }

    }
}