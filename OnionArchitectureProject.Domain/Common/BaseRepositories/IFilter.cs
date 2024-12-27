using System.Linq.Expressions;

namespace OnionArchitectureProject.Domain.Common.BaseRepositories;
public interface IFilter<TEntity>
{
    Expression<Func<TEntity, bool>> ToExpression();
}