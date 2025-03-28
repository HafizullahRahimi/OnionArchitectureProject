using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.Authentication;

namespace OnionArchitectureProject.Authentication.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IServiceScopeFactory _scopeFactory;

    public UserRepository(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    public async Task<string?> GetUserNameByUserIdAsync(string userId)
    {
        using var scope = _scopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.FindByIdAsync(userId);
        return user?.UserName;
    }
}