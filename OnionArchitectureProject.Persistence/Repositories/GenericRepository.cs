using OnionArchitectureProject.Application.Contracts.Persistence;
using System.Linq.Expressions;

namespace OnionArchitectureProject.Persistence.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public T GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public T GetByIdWithIncludes(int id)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdWithIncludesAsync(int id)
    {
        throw new NotImplementedException();
    }

    public bool Remove(int id)
    {
        throw new NotImplementedException();
    }

    public int Save()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveAsync()
    {
        throw new NotImplementedException();
    }

    public T Select(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<T> SelectAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }
}