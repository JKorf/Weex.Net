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
using Weex.Net.Interfaces.Clients.SpotApi;

namespace Weex.Net.Clients.SpotApi
{
    internal partial class WeexSocketClientSpotSharedApi :
        SharedApiBase,
        IWeexSocketClientSpotSharedApi,
        IWeexSocketClientSpotApiShared
    {
        private readonly WeexSocketClientSpotApi _api;

        private const string _topicId = "WeexSpot";
        private const string _exchangeName = "Weex";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(WeexExchange.Metadata, this);

        public WeexSocketClientSpotSharedApi(WeexSocketClientSpotApi api)
            : base(
                  api.Exchange,
                  [TradingMode.Spot],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeBalanceOptions,
                SubscribeBookTickerOptions,
                SubscribeKlineOptions,
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeUserTradeOptions,
                SubscribeSpotOrderOptions
                );
        }

    }
}
