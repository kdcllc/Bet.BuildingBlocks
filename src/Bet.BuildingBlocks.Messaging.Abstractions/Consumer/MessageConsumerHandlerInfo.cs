using System;

namespace Bet.BuildingBlocks.Messaging.Abstractions.Consumer
{
    public sealed class MessageConsumerHandlerInfo
    {
        public MessageConsumerHandlerInfo(bool isDynamic, Type handlerType)
        {
            IsDynamic = isDynamic;
            HandlerType = handlerType;
        }

        public bool IsDynamic { get; }

        public Type HandlerType { get; }

        public Type? MessageType { get; }

        public static MessageConsumerHandlerInfo Dynamic(Type handlerType)
        {
            return new MessageConsumerHandlerInfo(true, handlerType);
        }

        public static MessageConsumerHandlerInfo Typed(Type handlerType)
        {
            return new MessageConsumerHandlerInfo(false, handlerType);
        }
    }
}
