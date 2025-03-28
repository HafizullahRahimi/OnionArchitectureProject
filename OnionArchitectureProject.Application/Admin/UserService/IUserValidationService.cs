namespace OnionArchitectureProject.Application.Admin.UserService;

public interface IUserValidationService
{
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);
    Task<bool> IsUserNameUniqueAsync(string userName, CancellationToken cancellationToken);
}