using PayCorona.Models;
using Microsoft.EntityFrameworkCore;

namespace PayCorona.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) 
        {
            
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Wallet>().HasOne<Client>();
            modelBuilder.Entity<Transaction>().HasOne<Client>();

            var sessionEntity = modelBuilder.Entity<Session>();
            sessionEntity.HasKey(x => x.Id);
            sessionEntity.HasOne<Client>();
        }
    }
}
