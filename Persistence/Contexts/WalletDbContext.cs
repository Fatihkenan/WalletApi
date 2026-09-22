using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class WalletDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Integrated Security = True;mssqllocaldb;Database=WalletDb;TrustServerCertificate = True;");

        }
    }
}
