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
        /// Create a subscription with wire topics and optional local topic filters.
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="subTopics">Topics sent to WEEX in subscribe and unsubscribe requests</param>
        /// <param name="eventType">Event identifiers to route to this subscription</param>
        /// <param name="topicFilters">Local routing keys matching the message handler's topic mappings; null or empty matches only by event identifier</param>
        /// <param name="handler">Callback for matching messages</param>
        /// <param name="auth">Whether the subscription requires authentication</param>
        public WeexSubscription(
            ILogger logger,
            string[] subTopics,
            string[] eventType, 
            string[]? topicFilters,
            Action<DateTime, string?, T> handler,
            bool auth
            ) : base(logger, auth)
        {
            _handler = handler;
            _subTopics = subTopics;
            IndividualSubscriptionCount = _subTopics.Length;

            var routes = new List<MessageRoute>();
            foreach(var type in eventType)
            {
                if (topicFilters?.Length > 0)
                {
                    routes.AddRange(topicFilters.Select(x => MessageRoute.CreateForEvent<T>(type, x, DoHandleMessage)));
                }
                else
                {
                    routes.Add(MessageRoute.CreateForEvent<T>(type, DoHandleMessage));
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
            return CallResult.Ok();
        }
    }
}
