namespace OnionArchitectureProject.Domain.Common.BaseRepositories;
public interface IIncludes<TEntity>
{
    IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query);
}