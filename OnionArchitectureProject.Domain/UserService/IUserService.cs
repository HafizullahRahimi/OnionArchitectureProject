namespace OnionArchitectureProject.Domain.UserService;
public interface IUserService
{
    Task<string?> GetUserNemeByIdAsync(string userId);
}