using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Identity;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. DbContext'i sisteme ekle
            services.AddDbContext<WalletDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

            // 2. Identity'yi sisteme ekle ve DbContext'e bağla
            services.AddIdentityCore<AppUser>(options =>
            {
                // Şifre kurallarını esnetebilirsin (Opsiyonel)
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<AppRole>() // Rol altyapısını sisteme dahil ediyoruz
            .AddEntityFrameworkStores<WalletDbContext>();
        }
    }
}
