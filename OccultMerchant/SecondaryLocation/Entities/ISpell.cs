using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondaryLocation.Entities
{
    public interface ISpell
    {
        Guid id { get; set; }
        string name { get; set; }
        string description { get; set; }
        string source { get; set; }
        int price { get; set; }
        int ItemType { get; set; }
        int range { get; set; }
        string target { get; set; }
        string duration { get; set; }
        string savingThrow { get; set; }
        bool spellResistence { get; set; }
        string casting { get; set; }
        string component { get; set; }
        string school { get; set; }
        string level { get; set; }
    }
}