using System.Linq.Expressions;

namespace OnionArchitectureProject.Domain.Base.Repositories;
public interface IFilter<TEntity>
{
    Expression<Func<TEntity, bool>> ToExpression();
}