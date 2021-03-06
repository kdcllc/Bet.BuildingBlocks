using Bet.BuildingBlocks.Domain.Abstractions.Interfaces;

namespace Bet.BuildingBlocks.Domain.Abstractions.Specifications.Interfaces;

public interface IAsyncRepository<T> where T : BaseEntity, IAggregateRoot
{
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

    Task<T> FirstAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

    Task<T> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
}
