using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using System;
using Weex.Net.Objects.Internal;

namespace Weex.Net.Objects.Sockets.Subscriptions
{
    internal class WeexPingSubscription : SystemSubscription
    {
        public WeexPingSubscription(ILogger logger) : base(logger, false)
        {
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<WeexPing>("ping", HandlePing);
        }

        private CallResult? HandlePing(SocketConnection connection, DateTime time, string? arg3, WeexPing ping)
        {
            var id = ExchangeHelpers.NextId();
            _ = connection.SendAndWaitQueryAsync(new WeexQuery(new WeexSocketRequest() { Id = id, Method = "PONG" }, false));
            return CallResult.SuccessResult;
        }
    }
}
