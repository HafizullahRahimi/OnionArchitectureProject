using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Domain.Products;
using OnionArchitectureProject.Persistence.Repositories.ProductRepository;

namespace OnionArchitectureProject.Persistence;
public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        #region Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        #endregion

        return services;
    }
}