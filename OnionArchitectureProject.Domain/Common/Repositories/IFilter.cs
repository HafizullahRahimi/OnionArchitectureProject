using System.Linq.Expressions;

namespace OnionArchitectureProject.Domain.Common.Repositories;
public interface IFilter<TEntity>
{
    Expression<Func<TEntity, bool>> ToExpression();
}