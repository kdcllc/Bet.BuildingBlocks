namespace Bet.BuildingBlocks.Messaging.Abstractions.Consumer;

/// <summary>
/// Register Event Message <see cref="EventMessage" /> handler for event processing.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IMessageConsumerHandler<in TModel> : IMessageReceiverHandler where TModel : EventMessage
{
    /// <summary>
    /// Handles the Event Messages.
    /// </summary>
    /// <param name="message">The event message data received.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task HandleAsync(TModel message, CancellationToken cancellationToken = default);
}

public interface IMessageReceiverHandler
{
}
