using OnionArchitecture.Domain.Common;

namespace OnionArchitectureProject.Application.Contracts.Persistence.IRepositories.Common;
public interface IRepositoryWithFilter<TEntity> where TEntity : EntityBaseWithoutId, new()
{
    Task<TEntity?> GetAsync(IFilter<TEntity> filter, CancellationToken cancellationToken, IIncludes<TEntity>? includes = null);
}