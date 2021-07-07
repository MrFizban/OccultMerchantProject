using System;

namespace SecondaryLocation.Items
{
    public record Item : IItem
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public int price { get; set; }
        public int type { get; set; }
    }
}