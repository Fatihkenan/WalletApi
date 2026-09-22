using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class WalletDbContext : DbContext
    {

        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
        {

        }
        DbSet<Wallet> Wallets { get; set; }
        DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SenderWallet)
                .WithMany(w => w.SentTransactions)
                .HasForeignKey(t => t.SenderWalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.ReceiverWallet)
                .WithMany(w => w.ReceivedTransactions)
                .HasForeignKey(t => t.ReceiverWalletId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
