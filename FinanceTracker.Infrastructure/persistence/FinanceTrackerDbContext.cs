
using FinanceTracker.Infrastructure.persistence.entities;
using FinancialTransaction = FinanceTracker.Infrastructure.Persistence.entities.FinancialTransaction;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using FinanceTracker.Infrastructure.persistence.entities.FinanceTracker.Infrastructure.Persistence.Entities;

namespace FinanceTracker.Infrastructure.Persistence
{
    public class FinanceTrackerDbContext : DbContext
    {
        
        public FinanceTrackerDbContext(DbContextOptions<FinanceTrackerDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FinancialTransaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18, 2)"); // Toplam 18 basamak, virgülden sonra 2 basamak

           
            modelBuilder.Entity<Currency>()
                .Property(c => c.RateToTRY)
                .HasColumnType("decimal(18, 6)"); // virgülden sonra 6 basamak
        }

        
        public DbSet<FinancialTransaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
    }
}