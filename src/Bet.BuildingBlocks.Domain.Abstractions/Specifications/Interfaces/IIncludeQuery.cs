using Bet.BuildingBlocks.Domain.Abstractions.Specifications.Query;

namespace Bet.BuildingBlocks.Domain.Abstractions.Specifications.Interfaces;

public interface IIncludeQuery
{
    Dictionary<IIncludeQuery, string> PathMap { get; }

    IncludeVisitor Visitor { get; }

    HashSet<string> Paths { get; }
}

public interface IIncludeQuery<TEntity, out TPreviousProperty> : IIncludeQuery
{
}
