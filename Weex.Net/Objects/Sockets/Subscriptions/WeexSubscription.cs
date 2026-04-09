using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Weex.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class WeexSubscription<T> : Subscription
    {
        private readonly Action<DateTime, string?, T> _handler;
        private readonly string[] _subTopics;

        /// <summary>
        /// ctor
        /// </summary>
        public WeexSubscription(
            ILogger logger,
            string[] subTopics,
            string[] eventType, 
            string[]? symbols,
            Action<DateTime, string?, T> handler,
            bool auth
            ) : base(logger, auth)
        {
            _handler = handler;
            _subTopics = subTopics;

            var routes = new List<MessageRoute>();
            foreach(var type in eventType)
            {
                if (symbols?.Length > 0)
                {
                    routes.AddRange(symbols.Select(x => MessageRoute<T>.CreateWithTopicFilter(type, x, DoHandleMessage)));
                }
                else
                {
                    routes.Add(MessageRoute<T>.CreateWithoutTopicFilter(type, DoHandleMessage));
                }
            }

            MessageRouter = MessageRouter.Create(routes.ToArray());
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new WeexQuery(new WeexSocketRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "SUBSCRIBE",
                Parameters = _subTopics
            }, Authenticated);
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new WeexQuery(new WeexSocketRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "UNSUBSCRIBE",
                Parameters = _subTopics
            }, Authenticated);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, T message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return new CallResult(null);
        }
    }
}
