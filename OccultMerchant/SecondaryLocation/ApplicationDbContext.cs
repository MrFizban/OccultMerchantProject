using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SecondaryLocation.Entities;


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

     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Item>()
                .Property(e => e.id)
                .HasConversion(
                    g => g.ToString(),
                    s => Guid.Parse(s));

            modelBuilder
                .Entity<SpellWrappper>()
                .Property(e => e.id)
                .HasConversion(
                    g => g.ToString(),
                    s => Guid.Parse(s));

            modelBuilder
                .Entity<PotionWrapper>()
                .Property(e => e.id)
                .HasConversion(
                    g => g.ToString(),
                    s => Guid.Parse(s));
        }
        
        
        
    }
    
    
 

   
}