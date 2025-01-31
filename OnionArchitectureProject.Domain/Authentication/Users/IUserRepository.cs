using OnionArchitectureProject.Domain.Base.Repositories;

namespace OnionArchitectureProject.Domain.Authentication.Users;
public interface IUserRepository : IAuthRepository<User>
{
    Task<bool> UserNameExistsAsync(string userName);
    Task<bool> EmailExistsAsync(string email);
    Task<string?> GetUserNemeByIdAsync(string id);

    #region User status
    Task LockUserAsync(string id, int daysToLock);
    Task UnlockUserAsync(string id);
    Task SetUserStatusAsync(string id, bool isEnabled);
    #endregion

    #region Password management
    Task ChangePasswordAsync(string id, string currentPassword, string newPassword);
    Task<string?> GeneratePasswordResetTokenAsync(string email);
    Task ResetPasswordAsync(string id, string token, string newPassword);
    #endregion
}