using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bet.BuildingBlocks.Messaging.Abstractions.Consumer
{
    /// <summary>
    /// Used to defined registrations for <see cref="EventMessage"/>.
    /// </summary>
    public interface IMessageConsumerPool
    {
        /// <summary>
        /// Registered Handlers.
        /// </summary>
        ReadOnlyDictionary<string, List<MessageConsumerHandlerInfo>> Handlers { get; }

        /// <summary>
        /// Returns if the <see cref="IMessageConsumerPool"/> is empty.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Adds <see cref="IDynamicMessageConsumerHandler"/> to the pool based on the message name.
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="messageName">The message name to be added to be processed.</param>
        void AddDynamicHandler<THandler>(string messageName) where THandler : IDynamicMessageConsumerHandler;

        /// <summary>
        /// Removes <see cref="IDynamicMessageConsumerHandler"/> from the pool based on message name.
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="messageName"></param>
        void RemoveDynamicHandler<THandler>(string messageName) where THandler : IDynamicMessageConsumerHandler;

        /// <summary>
        /// Adds <see cref="IMessageConsumerHandler{TEventMessage}"/> to the pool.
        /// The name of the message is based on the generic type.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        void AddHandler<TModel, THandler>() where TModel : EventMessage where THandler : IMessageConsumerHandler<TModel>;

        /// <summary>
        /// Removes Message Handler from the pool.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        void RemoveHandler<TModel, THandler>() where TModel : EventMessage where THandler : IMessageConsumerHandler<TModel>;

        /// <summary>
        /// Returns status for Message Registration.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        bool IsRegistered<TModel>() where TModel : EventMessage;

        /// <summary>
        /// Returns status for Message Registration.
        /// </summary>
        /// <param name="messageName"></param>
        /// <returns></returns>
        bool IsRegistered(string messageName);

        /// <summary>
        /// Retrieves Type of the message based on the registered name.
        /// </summary>
        /// <param name="messageName"></param>
        /// <returns></returns>
        Type GeTypeByName(string messageName);

        /// <summary>
        /// Clears Handlers and Message Types dictionaries.
        /// </summary>
        void Clear();

        /// <summary>
        /// Returns all registered <see cref="MessageConsumerHandlerInfo"/> for a given message type.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        IEnumerable<MessageConsumerHandlerInfo> GetHandlers<TModel>() where TModel : EventMessage;

        /// <summary>
        /// Returns all registered <see cref="MessageConsumerHandlerInfo"/> for a given message name.
        /// </summary>
        /// <param name="messageName"></param>
        /// <returns></returns>
        IEnumerable<MessageConsumerHandlerInfo> GetHandlers(string messageName);

        /// <summary>
        /// Gets name of the generic.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string GetName<T>();
    }
}
