namespace OnionArchitectureProject.Domain.Authentication;

public interface IAuthenticationRepository
{
    Task<string?> GetUserNemeByUserIdAsync(string userId);
}