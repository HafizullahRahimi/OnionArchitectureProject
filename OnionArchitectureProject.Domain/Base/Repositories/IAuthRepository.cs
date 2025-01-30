namespace OnionArchitectureProject.Domain.Base.Repositories;
public interface IAuthRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(string id);
    Task<List<TEntity>> GetAllAsync();
    Task<bool> ExistAsync(string id);
    Task<TEntity> CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task DeleteAsync(string id);
}