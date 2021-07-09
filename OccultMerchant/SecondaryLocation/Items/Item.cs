using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondaryLocation.Items
{
    public class Item : IItem
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public int price { get; set; }
        [Column("type")]
        public int ItemType { get; set; }
    }
}