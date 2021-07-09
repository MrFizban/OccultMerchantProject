using System;
using System.ComponentModel;
using Microsoft.Extensions.Primitives;

namespace SecondaryLocation.Items
{
    public record Filter
    {
        public string name { get; init; }
        public string description { get; init; }
        public string source { get; init; }
        public (char op,int value) price { get; init; } 
        public ItemType itemType { get; init; }

  
    }
}