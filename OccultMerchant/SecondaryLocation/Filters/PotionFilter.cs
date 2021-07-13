using System;
using SecondaryLocation.Entities;

namespace SecondaryLocation.Filters
{
    public class PotionFilter 
    {
        public Guid? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        // op: operator per il confronto con il prezzo
        public char? priceOp { get; set; }
        public int? price { get; set; }
        public Guid? idSpell { get; set; }
        public string level { get; set; }
        // op: operator per il confronto con il livelllo
        public char? levelOp { get; set; }
        public int? casterLevell { get; set; }
        // op: operator per il confronto con il livelllo
        public char? wheightOp { get; set; }
        public int? wheight { get; set; }
    }
}