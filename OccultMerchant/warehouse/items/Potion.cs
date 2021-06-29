using System.Collections.Generic;

namespace OccultMerchant.items
{
    public class Potion : Spell
    {
        // incanteimo di riferimento
        public string potionName { get; set; }
        // livello dell incantatore che ha fatto la pozione
        public int levell { get; set; }

        public Potion() : base()
        {
            this.potionName = "";
            this.levell = 3;
        }

        public Potion(long id, string _name, string _description, string _source, Price _price, List<CasterClass> castersPossibility, string potionName, int levell, List<Component> componentList) : base(id, _name, _description, _source, _price, castersPossibility,componentList)
        {
            this.potionName = potionName;
            this.levell = levell;
        }
    }
    
}