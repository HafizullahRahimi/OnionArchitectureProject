namespace OnionArchitectureProject.Domain.AuthenticationService;
public interface IAuthenticationService
{
    Task<string?> GetUserNemeByIdAsync(string userId);
}