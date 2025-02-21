using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Application.Admin.CategoryService;
using OnionArchitectureProject.Application.Admin.CategoryService.Profiles;
using OnionArchitectureProject.Application.Admin.ProductService;
using OnionArchitectureProject.Application.Admin.RoleService;
using OnionArchitectureProject.Application.Admin.RoleService.Profiles;
using OnionArchitectureProject.Application.Admin.UserService;

namespace OnionArchitectureProject.Application;
public static class ApplicationServicesRegistration
{
    public static void AddApplication(this IServiceCollection services)
    {
        //services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(typeof(RoleProfile));
        services.AddAutoMapper(typeof(CategoryProfile));

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
    }
}