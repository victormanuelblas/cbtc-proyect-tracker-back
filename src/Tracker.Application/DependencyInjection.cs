using Microsoft.Extensions.DependencyInjection;
using Tracker.Application.Interfaces;
using Tracker.Application.Services;
using Tracker.Application.Mappings;

namespace Tracker.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IShipmentService, ShipmentService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}