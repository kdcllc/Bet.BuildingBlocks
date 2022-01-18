namespace Bet.BuildingBlocks.Messaging.Abstractions.Producer;

/// <summary>
/// Producer of Messages. By producer we mean sender.
/// </summary>
public interface IMessageProducer
{
    /// <summary>
    /// Sends a single message type of <see cref="EventMessage"/> thru the messaging system.
    /// </summary>
    /// <param name="event">The event data to be sent.</param>
    /// <param name="subject">The subject/label of the message is not specified the typeof value is used.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task SendMessageAsync(EventMessage @event, string subject = "", CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a batch of messages <see cref="EventMessage"/> type thru the messaging system.
    /// </summary>
    /// <param name="events">The events data to be sent.</param>
    /// <param name="subject">The subject/label of the message is not specified the typeof value is used.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task SendMessagesAsync(IList<EventMessage> @events, string subject = "", CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a single message that can be serialized.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="subject"></param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task SendMessageAsync<T>(T message, string subject = "", CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a batch of messages that can be serialized.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messages"></param>
    /// <param name="subject"></param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task SendMessagesAsync<T>(IList<T> messages, string subject = "", CancellationToken cancellationToken = default);
}
