using OnionArchitectureProject.Domain.Base.Repositories;

namespace OnionArchitectureProject.Domain.Authentication.ApplicationUsers;
public interface IApplicationUserRepository : IAuthenticationRepository<ApplicationUser>
{
    Task<string?> GetUserNameByIdAsync(string id, CancellationToken cancellationToken);
}