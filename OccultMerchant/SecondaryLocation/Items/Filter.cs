using Microsoft.Extensions.Primitives;

namespace SecondaryLocation.Items
{
    public record Filter
    {
        public string name { get; init; }
        public string description { get; init; }
        public string source { get; init; }
        public (char,int) price { get; init; }
        public ItemType itemType { get; init; }
    }
}