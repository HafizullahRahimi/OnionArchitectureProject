namespace OnionArchitectureProject.Domain.Authentication;

public interface IUserRepository
{
    Task<string?> GetUserNemeByUserIdAsync(string userId);
}