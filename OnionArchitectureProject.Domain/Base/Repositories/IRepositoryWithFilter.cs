using System.Linq.Expressions;

namespace OnionArchitectureProject.Domain.Base.Repositories;
public interface IRepositoryWithFilter<TEntity>
{
    Task<List<TEntity>> GetAllAsync(IFilter<TEntity> filter, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync(IIncludes<TEntity> includes, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync(IFilter<TEntity> filter, IIncludes<TEntity> includes,
        CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync(IIncludes<TEntity> includes, CancellationToken cancellationToken,
        int numberOfEntities);
    Task<List<TEntity>> GetAllAsync(IFilter<TEntity>? filter, IIncludes<TEntity>? includes,
        CancellationToken cancellationToken, int numberOfEntities = 0);

    Task<TEntity?> GetAsync(IFilter<TEntity> filter, CancellationToken cancellationToken,
        IIncludes<TEntity>? includes = null);
    Task<bool> AnyAsync(IFilter<TEntity> filter, CancellationToken cancellationToken);

    Task<TProjection?> GetWithProjectionAsync<TProjection>(IFilter<TEntity> filter, CancellationToken cancellationToken);
    Task<List<TProjection>> GetListWithProjectionAsync<TProjection>(IFilter<TEntity> filter,
        CancellationToken cancellationToken);
    Task<TProp?> GetProperty<TProp>(IFilter<TEntity> filter, Expression<Func<TEntity, TProp>> select,
        CancellationToken cancellationToken);
    Task<ICollection<TProp>> GetDistinctListOfRecentProperties<TProp>(IFilter<TEntity> filter, Expression<Func<TEntity, TProp>> select,
        int numberOfProperties,
        CancellationToken cancellationToken);
}