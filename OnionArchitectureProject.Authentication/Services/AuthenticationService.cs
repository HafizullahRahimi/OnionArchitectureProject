using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.AuthenticationService;

namespace OnionArchitectureProject.Authentication.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> userManager;

    public AuthenticationService(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<string?> GetUserNemeByIdAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return null;

        var user = await userManager.FindByIdAsync(userId);
        return user?.UserName ?? "Unknown User";
    }
}