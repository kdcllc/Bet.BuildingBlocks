namespace Bet.BuildingBlocks.Messaging.Abstractions.Consumer;

public interface IDynamicMessageConsumerHandler
{
    /// <summary>
    /// Handles event messages that are of dynamic types.
    /// </summary>
    /// <param name="message">The event message data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task HandleAsync(dynamic message, CancellationToken cancellationToken = default);
}
