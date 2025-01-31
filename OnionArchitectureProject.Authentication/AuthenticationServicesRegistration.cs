using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Authentication.Account;
using OnionArchitectureProject.Authentication.Interceptors;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Authentication.Repositories.RoleRepository;
using OnionArchitectureProject.Authentication.Repositories.RoleRepository.Profiles;
using OnionArchitectureProject.Application.Authentication.Roles;
using OnionArchitectureProject.Authentication.Repositories.UserRepository;
using OnionArchitectureProject.Domain.Authentication.Users;

namespace OnionArchitectureProject.Authentication;
public static class AuthenticationServicesRegistration
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCascadingAuthenticationState();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

        var connectionString = configuration
            .GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddSingleton<SoftDeletedInterceptor>();
        services.AddSingleton<CreatedInterceptor>();
        services.AddSingleton<ModifiedInterceptor>();

        services.AddDbContext<AuthenticationDbContext>(
            (sp, option) => option
            .UseSqlServer(connectionString)
            .AddInterceptors(sp.GetRequiredService<SoftDeletedInterceptor>())
            .AddInterceptors(sp.GetRequiredService<CreatedInterceptor>())
            .AddInterceptors(sp.GetRequiredService<ModifiedInterceptor>())
            );
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
        });

        #region Profiles
        services.AddAutoMapper(typeof(ApplicationRoleProfile));
        services.AddAutoMapper(typeof(ApplicationUserProfile));
        #endregion

        #region Repositories
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        #endregion

        return services;
    }
}