using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Base;
using OnionArchitectureProject.Domain.Base.Repositories;

namespace OnionArchitectureProject.Authentication.Repositories.Common;
public class AuthenticationRepository<TEntity> : IAuthenticationRepository<TEntity>
    where TEntity : class, IEntityBase<string>
{
    public readonly AuthenticationDbContext DbContext;
    public AuthenticationRepository(IDbContextFactory<AuthenticationDbContext> dbcontextFactory)
    {

        DbContext = dbcontextFactory.CreateDbContext();
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>()
            .SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DbContext.Set<TEntity>().Update(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DbContext.Set<TEntity>().Remove(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
            DbContext.Set<TEntity>().Remove(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistAsync(string id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        return entity != null;
    }

    private async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}