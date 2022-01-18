using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace Bet.BuildingBlocks.Messaging.Abstractions.Consumer;

public sealed class MessageConsumerPool : IMessageConsumerPool
{
    private readonly ConcurrentDictionary<string, List<MessageConsumerHandlerInfo>> _handlers = new();
    private readonly ConcurrentDictionary<string, Type> _messageTypes = new();

    public ReadOnlyDictionary<string, List<MessageConsumerHandlerInfo>> Handlers => new(_handlers);

    public ReadOnlyDictionary<string, Type> MessageTypes => new(_messageTypes);

    public bool IsEmpty => !_handlers.Keys.Any();

    public void Clear()
    {
        _handlers.Clear();
        _messageTypes.Clear();
    }

    public void AddDynamicHandler<THandler>(string messageName)
        where THandler : IDynamicMessageConsumerHandler
    {
        AddHandler(typeof(THandler), messageName, isDynamic: true);
    }

    public void RemoveDynamicHandler<THandler>(string messageName)
        where THandler : IDynamicMessageConsumerHandler
    {
        var handler = FindDynamic<THandler>(messageName);
        RemoveHandler(messageName, handler);
    }

    public void AddHandler<TModel, THandler>()
        where TModel : EventMessage
        where THandler : IMessageConsumerHandler<TModel>
    {
        var handlerType = typeof(THandler);
        var messageName = GetName<TModel>();

        AddHandler(handlerType, messageName, isDynamic: false);

        if (!_messageTypes.Any(x => x.Key == messageName))
        {
            var messageType = typeof(TModel);
            _messageTypes.TryAdd(messageName, messageType);
        }
    }

    public void RemoveHandler<TModel, THandler>()
        where TModel : EventMessage
        where THandler : IMessageConsumerHandler<TModel>
    {
        var handler = FindHandler<TModel, THandler>();
        var messageName = GetName<TModel>();

        RemoveHandler(messageName, handler);
    }

    public bool IsRegistered<TModel>() where TModel : EventMessage
    {
        var key = GetName<TModel>();
        return IsRegistered(key);
    }

    public bool IsRegistered(string messageName)
    {
        return _handlers.ContainsKey(messageName);
    }

    public Type GeTypeByName(string messageName)
    {
        return _messageTypes.SingleOrDefault(t => t.Key == messageName).Value;
    }

    public IEnumerable<MessageConsumerHandlerInfo> GetHandlers<TModel>() where TModel : EventMessage
    {
        var messageName = GetName<TModel>();
        return GetHandlers(messageName);
    }

    public IEnumerable<MessageConsumerHandlerInfo> GetHandlers(string messageName)
    {
        return _handlers[messageName];
    }

    public string GetName<T>()
    {
        return typeof(T).Name;
    }

    private void AddHandler(Type handlerType, string messageName, bool isDynamic)
    {
        if (!IsRegistered(messageName))
        {
            _handlers.TryAdd(messageName, new List<MessageConsumerHandlerInfo>());
        }

        if (_handlers[messageName].Any(s => s.HandlerType == handlerType))
        {
            throw new ArgumentException($"Handler Type {handlerType.Name} already registered for '{messageName}'", nameof(handlerType));
        }

        if (isDynamic)
        {
            _handlers[messageName].Add(MessageConsumerHandlerInfo.Dynamic(handlerType));
        }
        else
        {
            _handlers[messageName].Add(MessageConsumerHandlerInfo.Typed(handlerType));
        }
    }

    private void RemoveHandler(string messageName, MessageConsumerHandlerInfo? handler)
    {
        if (handler != null)
        {
            // single
            _handlers[messageName].Remove(handler);

            // others
            if (!_handlers[messageName].Any())
            {
                _handlers.TryRemove(messageName, out var v_);
                _messageTypes.TryRemove(messageName, out var _);
            }
        }
    }

    private MessageConsumerHandlerInfo? FindDynamic<THandler>(string messageName)
        where THandler : IDynamicMessageConsumerHandler
    {
        return FindHandler(messageName, typeof(THandler));
    }

    private MessageConsumerHandlerInfo? FindHandler<TEventMessage, TConsumerHandler>()
        where TEventMessage : EventMessage
        where TConsumerHandler : IMessageConsumerHandler<TEventMessage>
    {
        var messageName = GetName<TEventMessage>();
        return FindHandler(messageName, typeof(TConsumerHandler));
    }

    private MessageConsumerHandlerInfo? FindHandler(string messageName, Type handlerType)
    {
        if (!IsRegistered(messageName))
        {
            return null;
        }

        return _handlers[messageName].SingleOrDefault(s => s.HandlerType == handlerType);
    }
}
