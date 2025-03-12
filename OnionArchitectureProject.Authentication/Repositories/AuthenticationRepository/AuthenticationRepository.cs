using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.Authentication;

namespace OnionArchitectureProject.Authentication.Repositories.AuthenticationRepository;

public class AuthenticationRepository(UserManager<ApplicationUser> userManager) : IAuthenticationRepository
{
    private readonly UserManager<ApplicationUser> userManager = userManager;

    public async Task<string?> GetUserNemeByUserIdAsync(string userId)
    {
        var appUser = await GetAppUserByUserIdAsync(userId);
        return appUser?.UserName ?? null;
    }

    private async Task<ApplicationUser?> GetAppUserByUserIdAsync(string userId) =>
       await userManager.FindByIdAsync(userId);
}