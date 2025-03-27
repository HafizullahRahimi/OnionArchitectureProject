namespace OnionArchitectureProject.Domain.Authentication;

public interface IUserRepository
{
    Task<string?> GetUserNameByUserIdAsync(string userId);
    Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
}