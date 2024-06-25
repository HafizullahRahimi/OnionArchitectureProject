using System.Linq.Expressions;

namespace OnionArchitecture.Domain.Common.Repositories;
public interface IFilter<TEntity>
{
    Expression<Func<TEntity, bool>> ToExpression();
}