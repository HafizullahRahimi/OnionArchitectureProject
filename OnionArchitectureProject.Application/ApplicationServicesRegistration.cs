using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Application.Admin.CategoryService;
using OnionArchitectureProject.Application.Admin.CategoryService.Profiles;
using OnionArchitectureProject.Application.Admin.ProductService;
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
        #endregion

        #region Services
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUserValidationService, UserValidationService>();
        services.AddScoped<IUserService, UserService>();
        #endregion
    }
}