namespace OnionArchitectureProject.Domain.Base.Repositories;
public interface IAuthenticationRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> ExistAsync(string id, CancellationToken cancellationToken);
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}