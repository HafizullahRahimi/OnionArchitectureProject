using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Base;
using OnionArchitectureProject.Domain.Base.Repositories;
using System.Linq.Expressions;

namespace OnionArchitectureProject.Persistence.Repositories.Base;
public class RepositoryWithFilter<TEntity> : IRepositoryWithFilter<TEntity>
    where TEntity : CreatedEntity<Guid>, new()
{
    public readonly ApplicationDbContext DbContext;

    public RepositoryWithFilter(IDbContextFactory<ApplicationDbContext> dbcontextFactory)
    {
        DbContext = dbcontextFactory.CreateDbContext();
    }

    public async Task<List<TEntity>> GetAllAsync(IFilter<TEntity> filter, CancellationToken cancellationToken) =>
        await GetAllAsync(filter, null, cancellationToken, 0);

    public async Task<List<TEntity>> GetAllAsync(IIncludes<TEntity> includes, CancellationToken cancellationToken) =>
        await GetAllAsync(null, includes, cancellationToken, 0);

    public async Task<List<TEntity>> GetAllAsync(IFilter<TEntity> filter, IIncludes<TEntity> includes,
        CancellationToken cancellationToken) => await GetAllAsync(filter, includes, cancellationToken, 0);

    public async Task<List<TEntity>> GetAllAsync(IIncludes<TEntity> includes, CancellationToken cancellationToken,
        int numberOfEntities) => await GetAllAsync(null, includes, cancellationToken, numberOfEntities);

    public async Task<List<TEntity>> GetAllAsync(IFilter<TEntity>? filter, IIncludes<TEntity>? includes,
        CancellationToken cancellationToken, int numberOfEntities = 0)
    {
        IQueryable<TEntity> query = DbContext.Set<TEntity>()
            .OrderByDescending(e => e.CreatedUtcDate);

        if (filter != null)
        {
            query = query.Where(filter.ToExpression());
        }

        if (includes != null)
        {
            query = includes.ApplyIncludes(query);
        }

        if (numberOfEntities != 0)
        {
            query = query.Take(numberOfEntities);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(IFilter<TEntity> filter, CancellationToken cancellationToken,
        IIncludes<TEntity>? includes = null)
    {
        var query = DbContext.Set<TEntity>().Where(filter.ToExpression());
        if (includes != null)
        {
            query = includes.ApplyIncludes(query);
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(IFilter<TEntity> filter, CancellationToken cancellationToken) =>
    await DbContext.Set<TEntity>().AnyAsync(filter.ToExpression(), cancellationToken);

    public async Task<TProjection?> GetWithProjectionAsync<TProjection>(IFilter<TEntity> filter, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>()
            .Where(filter.ToExpression())
            .Select(entity => (TProjection)(object)entity)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<TProp>> GetDistinctListOfRecentProperties<TProp>(IFilter<TEntity> filter, Expression<Func<TEntity, TProp>> select,
        int numberOfProperties,
        CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>()
            .Where(filter.ToExpression())
            .OrderByDescending(e => e.CreatedUtcDate)
            .Select(select)
            .Distinct()
            .Take(numberOfProperties)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TProjection>> GetListWithProjectionAsync<TProjection>(IFilter<TEntity> filter, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>()
            .Where(filter.ToExpression())
            .Select(entity => (TProjection)(object)entity)
            .ToListAsync(cancellationToken);
    }

    public async Task<TProp?> GetProperty<TProp>(IFilter<TEntity> filter, Expression<Func<TEntity, TProp>> select,
        CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>()
            .Where(filter.ToExpression())
            .Select(select)
            .FirstOrDefaultAsync(cancellationToken);
    }
}