// FinanceTracker.API/DesignTimeDbContextFactory.cs

using FinanceTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FinanceTracker.API
{
   
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FinanceTrackerDbContext>
    {
        public FinanceTrackerDbContext CreateDbContext(string[] args)
        {
            
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // appsettings.json dosyasını yükle
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            // Connection string'i appsettings.json'dan oku
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // DbContextOptions'ı oluştur
            var builder = new DbContextOptionsBuilder<FinanceTrackerDbContext>();
            builder.UseSqlServer(connectionString);

            // DbContext'i döndür 
            return new FinanceTrackerDbContext(builder.Options);
        }
    }
}