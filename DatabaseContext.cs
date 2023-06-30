using CryptoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoApp
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<CoinUser> CoinUsers { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coin>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Coins)
                .UsingEntity<CoinUser>();
        }

        public DatabaseContext()
        {

        }
    }
}
