using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Authentication.Repositories.Common;
using OnionArchitectureProject.Domain.Authentication.ApplicationUsers;

namespace OnionArchitectureProject.Authentication.Repositories;
public class ApplicationUserRepository : AuthenticationRepository<ApplicationUser>, IApplicationUserRepository
{
    public ApplicationUserRepository(IDbContextFactory<AuthenticationDbContext> dbcontextFactory) : base(dbcontextFactory)
    {
    }

    public async Task<string?> GetUserNameByIdAsync(string id, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(id))
            return null;
        var user = await GetByIdAsync(id, cancellationToken);
        return user?.UserName;
    }
}