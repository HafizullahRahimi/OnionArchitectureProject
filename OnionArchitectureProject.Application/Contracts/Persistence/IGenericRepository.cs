using System.Linq.Expressions;

namespace OnionArchitectureProject.Application.Contracts.Persistence;
public interface IGenericRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    Task<List<T>> GetAllAsync();
    T GetById(int id);
    T GetByIdWithIncludes(int id);
    Task<T> GetByIdAsync(int id);
    Task<T> GetByIdWithIncludesAsync(int id);
    bool Remove(int id);
    void Add(T entity);
    void Update(T entity);
    int Save();
    Task<int> SaveAsync();
    public T Select(Expression<Func<T, bool>> predicate);
    public Task<T> SelectAsync(Expression<Func<T, bool>> predicate);
}