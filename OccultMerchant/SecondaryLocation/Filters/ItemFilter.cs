using System;
using SecondaryLocation.Entities;


namespace SecondaryLocation.Filters
{
    public class ItemFilter 
    {
        public Guid? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public int? price { get; set; }
        public int? ItemType { get; set; }
        
        // op: operator per il confronto con il prezzo
        public char? priceOp { get; set; }

    }
}