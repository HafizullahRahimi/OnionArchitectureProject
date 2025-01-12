using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Authentication.Account;
using OnionArchitectureProject.Authentication.Repositories;
using OnionArchitectureProject.Authentication.Services;
using OnionArchitectureProject.Domain.Authentication;
using OnionArchitectureProject.Domain.Authentication.ApplicationRole;

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

        services.AddDbContextFactory<AuthenticationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        #region Repositories
        services.AddScoped<IApplicationRoleRepository, ApplicationRoleRepository>();
        #endregion

        #region Servises
        services.AddScoped<IUserService, UserService>();
        #endregion

        return services;
    }
}