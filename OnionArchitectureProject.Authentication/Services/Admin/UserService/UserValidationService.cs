using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Application.Admin.UserService;
using OnionArchitectureProject.Authentication.Models;

namespace OnionArchitectureProject.Authentication.Services.Admin.UserService;

public class UserValidationService : IUserValidationService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public UserValidationService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        using var scope = _scopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.FindByEmailAsync(email);
        return user == null;
    }

    public async Task<bool> IsUserNameUniqueAsync(string userName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userName))
            return false;

        using var scope = _scopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.FindByNameAsync(userName);
        return user == null;
    }
}