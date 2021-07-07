using System;

namespace SecondaryLocation.Items
{
    public class Spell : Item,ISpell
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public int price { get; set; }
        public int ItemType { get; set; }
        public int range { get; set; }
        public string target { get; set; }
        public string duration { get; set; }
        public string savingThrow { get; set; }
        public bool spellResistence { get; set; }
        public string casting { get; set; }
        public string component { get; set; }
        public string school { get; set; }
        public string level { get; set; }
    }
}