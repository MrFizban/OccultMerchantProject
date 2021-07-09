using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SecondaryLocation.Items;

namespace SecondaryLocation
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Item> Item { get; set; }
        public DbSet<SpellWrappper> Spell { get; set; }
        public DbSet<PotionWrapper> Potion { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }

    
    
    public class PotionWrapper
    {
        [Key]
        public Guid id { get; set; }
        public int casterLevel { get; set; }
        public int wheight { get; set; }
    }

    public class SpellWrappper
    {
        [Key]
        public Guid id { get; set; }
        public int range { get; set; }
        public string target { get; set; }
        public string duration { get; set; }
        public string savingThrow { get; set; }
        public bool spellResistence { get; set; }
        public string casting { get; set; }
        public string component { get; set; }
        public string school { get; set; }
        public string level { get; set; }

    }
}