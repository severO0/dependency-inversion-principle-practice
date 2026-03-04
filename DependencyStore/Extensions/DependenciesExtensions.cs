using DependencyStore.Repositories;
using DependencyStore.Repositories.Contracts;
using DependencyStore.Services;
using DependencyStore.Services.Contratcs;
using Microsoft.Data.SqlClient;

namespace DependencyStore.Extensions
{
    public static class DependenciesExtensions
    {

        public static void AddSqlConnection(this IServiceCollection services)
        {
            services.AddScoped(conn
                =>
            {
                var configuration = conn.GetRequiredService<IConfiguration>();
                var connStr = configuration.GetConnectionString("DefaultConnection");
                return new SqlConnection(connStr);
            });
        }

        public static void AddConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton(configuration);
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IPromoCodeRepository, PromoCodeRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IDeliveryFeeService, DeliveryFeeService>();
        }
    }
}
