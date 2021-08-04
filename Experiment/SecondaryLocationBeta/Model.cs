using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SecondaryLocation.Entities;

namespace EFGetStarted
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Item> Item { get; set; }
        public DbSet<Spell> Spell { get; set; }

        // The following configures EF to create a Sqlite database file as `C:\blogging.db`.
        // For Mac or Linux, change this to `/tmp/blogging.db` or any other absolute path.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\blogging.db");
    }
}