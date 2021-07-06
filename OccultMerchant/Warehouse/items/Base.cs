using warehouse.Controllers;
using Warehouse.Database;
using Warehouse.items;

namespace Warehouse.items
{
    public abstract class Base
    {
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public Price price { get; set; }
        public long id { get; set; }

        public Filter filter { get; set; }

        public Base() 
        {
            this.id = -1;
            this.name = "";
            this.description = "";
            this.source = "";
            this.price = new Price();
            this.filter = new Filter();
        }

        public Base(long id = 0)
        {
            this.id = id;
            this.name = "Gianni";
            this.description = "Gianni è un cavallo buffo";
            this.source = "homebrew";
            this.price = new Price(42, CoinType.SilverCoin);
            this.filter = new Filter();
        }

        public Base(long id, string _name, string _description, string _source, Price _price, Filter filter)
        {
            this.id = id;
            this.name = _name;
            this.description = _description;
            this.source = _source;
            this.price = _price;
            this.filter = filter;
        }

        
    }
}