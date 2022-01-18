namespace Bet.BuildingBlocks.Domain.Abstractions.Interfaces;

/// <summary>
/// Repository for the aggregate root entity.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
