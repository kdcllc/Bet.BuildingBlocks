namespace Bet.BuildingBlocks.Domain.Abstractions.Interfaces;

/// <summary>
/// This is a marker interface only to be applied to aggregate root entities.
/// The repositories must only work with aggregate roots, and not their children.
/// </summary>
public interface IAggregateRoot
{
}
