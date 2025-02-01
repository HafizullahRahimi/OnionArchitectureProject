using OnionArchitectureProject.Domain.Base.Repositories;
using System.Linq.Expressions;

namespace OnionArchitectureProject.Persistence.Models.Base.Repositories;
public class Filter<TEntity> : IFilter<TEntity>
{
    private readonly Expression<Func<TEntity, bool>> _expression;

    public Filter(Expression<Func<TEntity, bool>> expression)
    {
        _expression = expression;
    }
    public Expression<Func<TEntity, bool>> ToExpression() => _expression;
}