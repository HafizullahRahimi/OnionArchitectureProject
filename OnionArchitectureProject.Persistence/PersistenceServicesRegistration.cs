using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Domain.Products;
using OnionArchitectureProject.Persistence.Interceptors;
using OnionArchitectureProject.Persistence.Repositories;
using OnionArchitectureProject.Persistence.Repositories.ProductRepository;
using System;

namespace OnionArchitectureProject.Persistence;
public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddSingleton<SoftDeletedInterceptor>();
        services.AddSingleton<CreatedInterceptor>();
        services.AddSingleton<ModifiedInterceptor>();

        services.AddDbContext<ApplicationDbContext>(
            (sp, option) => option
            .UseSqlServer(connectionString)
            .AddInterceptors(sp.GetRequiredService<SoftDeletedInterceptor>())
            .AddInterceptors(sp.GetRequiredService<CreatedInterceptor>())
            .AddInterceptors(sp.GetRequiredService<ModifiedInterceptor>())
            );

        //services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        #region Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        #endregion

        return services;
    }
}