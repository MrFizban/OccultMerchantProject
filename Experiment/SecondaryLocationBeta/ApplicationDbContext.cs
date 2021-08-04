using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DbSet<Spell> Spell { get; set; }
        
        public DbSet<Potion> Potion { get; set; }
        
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
                .Entity<Spell>()
                .Property(e => e.id)
                .HasConversion(
                    g => g.ToString(),
                    s => Guid.Parse(s));
            
            modelBuilder
                .Entity<Potion>()
                .Property(e => e.id)
                .HasConversion(
                    g => g.ToString(),
                    s => Guid.Parse(s));


            modelBuilder.Entity<Spell>()
                .HasOne<Item>(spell =>  spell.Item)
                .WithOne(item => item.spell)
                .HasForeignKey<Spell>(ad => ad.ItemId);
            
            modelBuilder.Entity<Potion>()
                .HasOne<Item>(potion =>  potion.Item)
                .WithOne(item => item.potion)
                .HasForeignKey<Potion>(potion => potion.ItemId);


        }
    }
}