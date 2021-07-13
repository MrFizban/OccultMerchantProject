using System;
using SecondaryLocation.Entities;

namespace SecondaryLocation.Filters
{
    public class SpellFilter
    {
        public Guid? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        // op: operator per il confronto con il prezzo
        public char? priceOp { get; set; }
        public int? price { get; set; }
        // op: operator per il confronto con il range
        public char? rangeOp { get; set; }
        public int? range { get; set; }
        public string target { get; set; }
        public string duration { get; set; }
        public string savingThrow { get; set; }
        public bool? spellResistence { get; set; }
        public string casting { get; set; }
        public string component { get; set; }
        public string school { get; set; }
        public string level { get; set; }
    }
}