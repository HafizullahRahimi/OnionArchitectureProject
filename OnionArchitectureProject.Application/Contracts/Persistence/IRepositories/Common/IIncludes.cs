namespace OnionArchitectureProject.Application.Contracts.Persistence.IRepositories.Common;
public interface IIncludes<TEntity>
{
    IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query);
}