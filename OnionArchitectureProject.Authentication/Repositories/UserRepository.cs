using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.Authentication;

namespace OnionArchitectureProject.Authentication.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbContextFactory<AuthenticationDbContext> dbContextFactory;

    public UserRepository(IDbContextFactory<AuthenticationDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    public async Task<string?> GetUserNameByUserIdAsync(string userId)
    {
        ArgumentNullException.ThrowIfNull(userId);
        var appUser = await GetByUserIdAsync(userId, CancellationToken.None);
        return appUser?.UserName;
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(email);
        return await GetByEmailAsync(email, cancellationToken) != null;
    }

    public async Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userName);
        return await GetByUserNameAsync(userName, cancellationToken) != null;
    }

    public async Task<bool> ExistsByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId);
        return await GetByUserIdAsync(userId, cancellationToken) != null;
    }

    private async Task<ApplicationUser?> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        return await dbContext.Set<ApplicationUser>()
            .SingleOrDefaultAsync(c => c.Id == userId, cancellationToken);
    }

    private async Task<ApplicationUser?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        return await dbContext.Set<ApplicationUser>()
            .SingleOrDefaultAsync(c => c.UserName == userName, cancellationToken);
    }

    private async Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        return await dbContext.Set<ApplicationUser>()
            .SingleOrDefaultAsync(c => c.Email == email, cancellationToken);
    }
}