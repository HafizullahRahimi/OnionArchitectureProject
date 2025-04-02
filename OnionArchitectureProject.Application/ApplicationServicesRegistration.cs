using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Application.Admin.CategoryService;
using OnionArchitectureProject.Application.Admin.CategoryService.Profiles;
using OnionArchitectureProject.Application.Admin.ProductService;
using OnionArchitectureProject.Application.Admin.RoleService;
using OnionArchitectureProject.Application.Admin.RoleService.Models;
using OnionArchitectureProject.Application.Admin.UserService;
using OnionArchitectureProject.Application.Admin.UserService.Models;

namespace OnionArchitectureProject.Application;
public static class ApplicationServicesRegistration
{
    public static void AddApplication(this IServiceCollection services)
    {
        #region Profiles
        //services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(typeof(CategoryProfile));
        services.AddAutoMapper(typeof(UserProfile));
        services.AddAutoMapper(typeof(RoleProfile));
        #endregion

        #region Services
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUserValidationService, UserValidationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleValidationService, RoleValidationService>();
        services.AddScoped<IRoleService, RoleService>();
        #endregion
    }
}