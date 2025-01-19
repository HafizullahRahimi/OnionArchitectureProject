using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Application.Admin.ApplicationRoleService;
using OnionArchitectureProject.Application.Admin.ApplicationRoleService.Profiles;
using OnionArchitectureProject.Application.Admin.CategoryService;
using OnionArchitectureProject.Application.Admin.CategoryService.Profiles;
using OnionArchitectureProject.Application.Admin.ProductService;
using System.Reflection;

namespace OnionArchitectureProject.Application;
public static class ApplicationServicesRegistration
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(typeof(RoleProfile));
        services.AddAutoMapper(typeof(CategoryProfile));

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IApplicationRoleService, ApplicationRoleService>();
    }
}