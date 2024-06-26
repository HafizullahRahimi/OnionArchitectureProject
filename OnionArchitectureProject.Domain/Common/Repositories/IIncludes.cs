namespace OnionArchitectureProject.Domain.Common.Repositories;
public interface IIncludes<TEntity>
{
    IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query);
}