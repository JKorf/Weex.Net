using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
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
            // Private connection doesn't send a response to out pong, no need to wait
            if (connection.ConnectionUri.PathAndQuery.Contains("/private"))
                _ = connection.SendAsync(id, new WeexSocketRequest() { Id = id, Method = "PONG" }, 0);
            else
                _ = connection.SendAndWaitQueryAsync(new WeexQuery(new WeexSocketRequest() { Id = id, Method = "PONG" }, false, 0));
            return CallResult.SuccessResult;
        }
    }
}
