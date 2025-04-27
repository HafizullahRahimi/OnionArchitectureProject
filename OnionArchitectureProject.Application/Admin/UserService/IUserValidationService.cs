namespace OnionArchitectureProject.Application.Admin.UserService;

public interface IUserValidationService
{
    Task<bool> IsEmailUniqueAsync(string email, string userId, CancellationToken cancellationToken);
    Task<bool> IsUserNameUniqueAsync(string userName, string userId, CancellationToken cancellationToken);
}