namespace OccultMerchant.items
{
    public class Potion : Base
    {
        // incanteimo di riferimento
        public Spell spell { get; set; }
        // livello dell incantatore che ha fatto la pozione
        public int levell { get; set; }

        public Potion() : base()
        {
            this.spell = new Spell();
            this.levell = 3;
        }
        
        public Potion(int id, string _name, string _description, string _source, Price _price, Spell spell, int levell) : base(id,_name, _description, _source, _price)
        {
            this.spell = spell;
            this.levell = levell;
        }
    }
    
}