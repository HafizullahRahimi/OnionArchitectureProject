using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Domain.Authentication;
using OnionArchitectureProject.Domain.IServices;

namespace OnionArchitectureProject.Authentication.Account.UserService;
public class UserServiceBase : IUserServiceBase
{
    private readonly UserManager<ApplicationUser> userManager;

    public UserServiceBase(UserManager<ApplicationUser> userManager)
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