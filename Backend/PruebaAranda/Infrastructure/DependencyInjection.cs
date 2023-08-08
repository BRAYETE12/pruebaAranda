using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConexion"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)),
                ServiceLifetime.Transient);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }

    }

}
