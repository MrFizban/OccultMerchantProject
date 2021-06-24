namespace OccultMerchant.items
{
    public class Base
    {
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public Price price { get; set; }

        public Base()
        {
            this.name = "Gianni";
            this.description = "Gianni è un cavallo buffo";
            this.source = "homebrew";
            this.price = new Price(42, CoinType.SilverCoin);
        }

        public Base(string _name, string _description, string _source, Price _price)
        {
            this.name = _name;
            this.description = _description;
            this.source = _source;
            this.price = _price;
        }
    }
}