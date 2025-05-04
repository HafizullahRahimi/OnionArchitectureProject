namespace OnionArchitectureProject.Domain.Authentication.Users;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(string id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string userName);
    Task<bool> ExistAsync(string id);
    Task<bool> UserNameExistsAsync(string userName);
    Task<bool> EmailExistsAsync(string email);
    Task<User> CreateAsync(User user, string password);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task DeleteAsync(string id);
    Task<bool> ChangePasswordAsync(string userId, string newPassword);
    Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    Task<IList<string>?> GetRolesAsync(string userId);
    Task AddToRolesAsync(string userId, IList<string> newRoles);
    Task RemoveFromRolesAsync(string userId, IList<string> currentRoles);
    Task UpdateUserRolesAsync(string userId, IList<string> newRoles);
}