using System.Linq.Expressions;

namespace OnionArchitectureProject.Application.Contracts.Persistence.IRepositories.Common;
public interface IFilter<TEntity>
{
    Expression<Func<TEntity, bool>> ToExpression();
}