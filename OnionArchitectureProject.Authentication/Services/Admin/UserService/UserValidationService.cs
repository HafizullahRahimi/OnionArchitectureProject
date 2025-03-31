using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Application.Admin.UserService;
using OnionArchitectureProject.Domain.Authentication.Users;

namespace OnionArchitectureProject.Authentication.Services.Admin.UserService;

public class UserValidationService : IUserValidationService
{
    private readonly IServiceScopeFactory scopeFactory;

    public UserValidationService(IServiceScopeFactory scopeFactory)
    {
        this.scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        using var scope = scopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var emailExists = await userRepository.EmailExistsAsync(email);
        return emailExists == false;
    }

    public async Task<bool> IsUserNameUniqueAsync(string userName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userName))
            return false;

        using var scope = scopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var userNameExists = await userRepository.UserNameExistsAsync(userName);
        return userNameExists == false;
    }
}