namespace SecondaryLocation.Items
{
    public interface ISpell : IItem
    {
        public int range { get; set; }
        public string target { get; set; }
        public string duration { get; set; }
        public string savingTrow { get; set; }
        public bool spellResistence { get; set; }
        public string casting { get; set; }
        public string component { get; set; }
        public string shcool { get; set; }
        public string level { get; set; }
    }
}