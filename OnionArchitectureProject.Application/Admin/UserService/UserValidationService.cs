using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Domain.Authentication.Users;

namespace OnionArchitectureProject.Application.Admin.UserService;

public class UserValidationService : IUserValidationService
{
    private readonly IServiceScopeFactory scopeFactory;

    public UserValidationService(IServiceScopeFactory scopeFactory)
    {
        this.scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    public async Task<bool> IsEmailUniqueAsync(string email, string userId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        using var scope = scopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        if (!string.IsNullOrEmpty(userId))
        {
            var currentUser = await userRepository.GetByIdAsync(userId);
            if (currentUser == null)
                return false;

            if (currentUser.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        var emailExists = await userRepository.EmailExistsAsync(email);
        return emailExists == false;
    }

    public async Task<bool> IsUserNameUniqueAsync(string userName, string userId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userName))
            return false;

        using var scope = scopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        if (!string.IsNullOrEmpty(userId))
        {
            var currentUser = await userRepository.GetByIdAsync(userId);
            if (currentUser == null)
                return false;

            if (currentUser.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        var userNameExists = await userRepository.UserNameExistsAsync(userName);
        return userNameExists == false;
    }
}