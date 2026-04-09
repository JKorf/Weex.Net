using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Microsoft.Extensions.Logging;
using System;
using Weex.Net.Objects.Internal;

namespace Weex.Net.Objects.Sockets.Subscriptions
{
    internal class WeexConnectedSubscription : SystemSubscription
    {
        public WeexConnectedSubscription(ILogger logger) : base(logger, false)
        {
            MessageRouter = MessageRouter.CreateWithoutHandler<WeexPing>("connected");
        }
    }
}
