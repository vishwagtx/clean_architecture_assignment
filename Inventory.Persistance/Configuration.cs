using Inventory.Application.Intefaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Persistance
{
    public static class Configuration
    {
        public static IServiceCollection AddEFConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetValue<string>("defaultConnectionString");
            services.AddDbContext<InventoryDbContext>(opts =>
             opts.UseSqlServer(connectionString)
            );

            var sp = services.BuildServiceProvider();
            var dbContext = sp.GetService<InventoryDbContext>();

            dbContext.Database.Migrate();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
