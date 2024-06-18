using System.Linq.Expressions;

namespace OnionArchitectureProject.Application.Contracts.Persistence.IRepositories;
public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>> GetAllAsync();
    Task<bool> ExistAsync(Guid id);
    Task<TEntity> Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void RemoveById(Guid id);
    Task SaveChangesAsync();
}