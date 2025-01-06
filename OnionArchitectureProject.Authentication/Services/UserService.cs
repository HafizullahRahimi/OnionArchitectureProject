using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Authentication;

namespace OnionArchitectureProject.Authentication.Services;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    #region Users management
    public async Task<List<ApplicationUser>> GetUsersAsync(int pageNumber, int pageSize)
    {
        return await Task.FromResult(
            userManager.Users
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList()
        );
    }

    public async Task<List<ApplicationUser>> GetUsersAsync() =>
        await userManager.Users.ToListAsync();

    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
    {
        return await userManager.FindByIdAsync(userId);
    }

    public async Task<string?> GetUserNemeByIdAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return null;
        var user = await userManager.FindByIdAsync(userId);
        return user?.UserName ?? null;
    }

    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityResult> UpdateUserAsync(string userId, string newEmail, string newUserName)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });

        user.Email = newEmail;
        user.UserName = newUserName;

        return await userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> DeleteUserAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });

        return await userManager.DeleteAsync(user);
    }
    #endregion

    #region Password management
    public async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });

        return await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    public async Task<string> GeneratePasswordResetTokenAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            throw new KeyNotFoundException("User not found.");

        return await userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });

        return await userManager.ResetPasswordAsync(user, token, newPassword);
    }
    #endregion

    #region User status
    public async Task<IdentityResult> LockUserAsync(string userId, int daysToLock)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });

        return await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddDays(daysToLock));
    }

    public async Task<IdentityResult> UnlockUserAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });

        return await userManager.SetLockoutEndDateAsync(user, null);
    }

    public async Task<IdentityResult> SetUserStatusAsync(string userId, bool isEnabled)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });

        user.LockoutEnabled = !isEnabled;
        return await userManager.UpdateAsync(user);
    }
    #endregion
}