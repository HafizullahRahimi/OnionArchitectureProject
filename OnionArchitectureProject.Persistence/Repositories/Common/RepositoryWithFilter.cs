using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Common;
using OnionArchitectureProject.Application.Contracts.Persistence.IRepositories.Common;

namespace OnionArchitectureProject.Persistence.Repositories.Common;
public class RepositoryWithFilter<TEntity> where TEntity : EntityBase<Guid>, new()
{
    public readonly ApplicationDbContext DbContext;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryWithFilter(ApplicationDbContext context)
    {
        DbContext = context;
        _dbSet = context.Set<TEntity>();
    }
    public async Task<TEntity?> GetAsync(IFilter<TEntity> filter, CancellationToken cancellationToken, IIncludes<TEntity>? includes = null)
    {
        var query = DbContext.Set<TEntity>().Where(filter.ToExpression());
        if (includes != null)
        {
            query = includes.ApplyIncludes(query);
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
