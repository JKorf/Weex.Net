using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Objects.Models;

namespace Weex.Net.Clients.FuturesApi
{
    internal partial class WeexRestClientFuturesSharedApi :
        SharedApiBase,
        IWeexRestClientFuturesApiShared,
        IWeexRestClientFuturesSharedApi
    {
        private readonly WeexRestClientFuturesApi _api;

        private const string _topicId = "WeexFutures";
        private const string _exchangeName = "Weex";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(WeexExchange.Metadata, this);

        public WeexRestClientFuturesSharedApi(WeexRestClientFuturesApi api)
            : base(
                  api.Exchange,
                  [TradingMode.PerpetualLinear],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetBalancesOptions,
                GetBookTickerOptions,
                GetFeeOptions,
                GetKlinesOptions,
                GetOrderBookOptions,
                GetRecentTradesOptions,
                GetFundingRateHistoryOptions,
                GetFuturesSymbolsOptions,
                GetFuturesTickerOptions,
                GetAllFuturesTickersOptions,
                GetMarkPriceKlinesOptions,
                GetIndexPriceKlinesOptions,
                GetLeverageOptions,
                SetLeverageOptions,
                GetOpenFuturesOrdersOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                GetFuturesOrderTradesOptions,
                GetFuturesUserTradeHistoryOptions,
                CancelFuturesOrderOptions,
                GetPositionsOptions,
                ClosePositionOptions,
                PlaceFuturesTriggerOrderOptions,
                GetFuturesTriggerOrderOptions,
                CancelFuturesTriggerOrderOptions,
                GetOpenInterestOptions
                );
        }

    }
}
