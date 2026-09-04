using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Interfaces.Clients.FuturesApi;

namespace Weex.Net.Clients.FuturesApi
{
    internal partial class WeexSocketClientFuturesSharedApi :
        SharedApiBase,
        IWeexSocketClientFuturesApiShared,
        IWeexSocketClientFuturesSharedApi
    {
        private readonly WeexSocketClientFuturesApi _api;

        private const string _topicId = "WeexFutures";
        private const string _exchangeName = "Weex";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(WeexExchange.Metadata, this);

        public WeexSocketClientFuturesSharedApi(WeexSocketClientFuturesApi api)
            : base(
                  SharedTransport.Socket,
                  api.Exchange,
                  [TradingMode.PerpetualLinear],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeBalanceOptions,
                SubscribeKlineOptions,
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeUserTradeOptions,
                SubscribeFuturesOrderOptions,
                SubscribePositionOptions
                );
        }

    }
}
