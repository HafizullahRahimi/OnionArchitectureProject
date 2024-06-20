using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Common;

namespace OnionArchitectureProject.Persistence.Repositories.Common;
public abstract class Repository<TEntity>
    where TEntity : EntityBase<Guid>
{
    public readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await DbContext.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<TEntity>()
            .SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        await DbContext.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }

    public async void RemoveById(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
            DbContext.Set<TEntity>().Remove(entity);
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        return entity != null;
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}