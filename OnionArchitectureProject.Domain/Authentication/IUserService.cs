using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Domain.Authentication.ApplicationUsers;

namespace OnionArchitectureProject.Domain.Authentication;
public interface IUserService
{
    Task<List<ApplicationUser>> GetUsersAsync(int pageNumber, int pageSize);
    Task<List<ApplicationUser>> GetUsersAsync();
    Task<ApplicationUser?> GetUserByIdAsync(string userId);
    Task<string?> GetUserNemeByIdAsync(string userId);
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
    Task<IdentityResult> UpdateUserAsync(string userId, string newEmail, string newUserName);
    Task<IdentityResult> DeleteUserAsync(string userId);
    Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    Task<string> GeneratePasswordResetTokenAsync(string email);
    Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);
    Task<IdentityResult> LockUserAsync(string userId, int daysToLock);
    Task<IdentityResult> UnlockUserAsync(string userId);
    Task<IdentityResult> SetUserStatusAsync(string userId, bool isEnabled);
}