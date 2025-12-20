using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tracker.Infrastructure.Persistence.Repositories;
using Tracker.Infrastructure.Persistence.Context;
using Tracker.Domain.Ports.Out;

namespace Tracker.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "1433";
            var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "TrackerDb";
            var user = Environment.GetEnvironmentVariable("DB_USER") ?? "sa";
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "Your_password123";

            var connectionString = $"Server={host},{port};Database={database};User Id={user};Password={password};";

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 0, 0))
                ));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IShipmentRepository, ShipmentRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
