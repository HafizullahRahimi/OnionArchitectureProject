using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.UserService;

namespace OnionArchitectureProject.Authentication.Services;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<string?> GetUserNemeByIdAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return null;
        var user = await userManager.FindByIdAsync(userId);
        return user?.UserName ?? null;
    }
}