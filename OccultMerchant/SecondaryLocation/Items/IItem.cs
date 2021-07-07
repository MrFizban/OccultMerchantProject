using System;

namespace SecondaryLocation.Items
{
    public interface IItem
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public int price { get; set; }
        public int ItemType { get; set; }
    }
    


}