namespace OnionArchitectureProject.Domain.Authentication;

public interface IUserRepository
{
    Task<string?> GetUserNameByUserIdAsync(string userId);
}