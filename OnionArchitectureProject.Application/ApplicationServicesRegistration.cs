using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Application.Admin.CategoryService;
using OnionArchitectureProject.Application.Admin.ProductService;
using System.Reflection;

namespace OnionArchitectureProject.Application;
public static class ApplicationServicesRegistration
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
    }
}