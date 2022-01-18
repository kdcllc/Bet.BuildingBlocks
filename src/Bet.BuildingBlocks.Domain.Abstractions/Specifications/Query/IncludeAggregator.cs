using System.Linq.Expressions;

using Bet.BuildingBlocks.Domain.Abstractions.Specifications.Interfaces;

namespace Bet.BuildingBlocks.Domain.Abstractions.Specifications.Query;

public class IncludeAggregator<TEntity>
{
    public IncludeQuery<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> selector)
    {
        var visitor = new IncludeVisitor();
        visitor.Visit(selector);

        var pathMap = new Dictionary<IIncludeQuery, string>();
        var query = new IncludeQuery<TEntity, TProperty>(pathMap);

        if (!string.IsNullOrEmpty(visitor.Path))
        {
            pathMap[query] = visitor.Path;
        }

        return query;
    }
}
