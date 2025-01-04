namespace OnionArchitectureProject.Domain.IServices;
public interface IUserServiceBase
{
    Task<string?> GetUserNemeByIdAsync(string userId);
}