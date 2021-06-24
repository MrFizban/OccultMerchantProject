using System.Collections.Generic;

namespace OccultMerchant.items
{
    public struct CasterClass
    {
        // il livello a cui viene castato l'incantesimo
        public int spellLevel { get; set; }
        // il nome della classe del caster
        public string casterName { get; set; }

        public CasterClass(int spellLevel, string casterName)
        {
            this.spellLevel = spellLevel;
            this.casterName = casterName;
        }
    }
    public class Spell : Base
    {
        // quali caste possono usare l'incantesimo e a che livello
        public List<CasterClass> castersPossibility { get; set; }

        public Spell() : base()
        {
            this.castersPossibility = new List<CasterClass>()
            {
                new CasterClass(2,"Mirto")
            };
        }

        public Spell(string _name, string _description, string _source, Price _price, List<CasterClass> castersPossibility) : base(_name, _description, _source, _price)
        {
            this.castersPossibility = castersPossibility;
        }
    }
}