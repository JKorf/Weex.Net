using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Clients.FuturesApi;
using Weex.Net.Enums;
using Weex.Net.Interfaces.Clients.SpotApi;
using Weex.Net.Objects.Models;

namespace Weex.Net.Clients.SpotApi
{
    internal partial class WeexRestClientSpotSharedApi :
        SharedApiBase,
        IWeexRestClientSpotApiShared,
        IWeexRestClientSpotSharedApi
    {
        private readonly WeexRestClientSpotApi _api;

        private const string _topicId = "WeexSpot";
        private const string _exchangeName = "Weex";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(WeexExchange.Metadata, this);

        private static readonly HashSet<string> _knownCommodities = 
            ["GOLD(PAXG)", "GOLDXAUT", "SILVER(XAG)", "SLVON", "PALLON",
            "IAUON", "PLATINUM(XPT)", "USOON", "CRUDEOIL"];

        private static readonly HashSet<string> _assetsEndingWithOnNotStocks = [
            "COON", "FON", "SOON", "NEON", "NEON", "ANON", "ELON", "PYTHON", "ONON", "KAON",
            "DRGON", "ROOMCON", "DIGIMON", "ALON", "BARRON", "CATTOn", "ENRON", "MILTON", "LION",
            "MON", "AUCTION", "CATTON", "CON", "ON", "UNION", "ATTENTION", "TERRAFORMATION",
            "DRAGON", "BACON", "LUCKYMOON", "POSEIDON", "LEMON", "MOTION", "COMMON", "TYCOON",
            ];

        public WeexRestClientSpotSharedApi(WeexRestClientSpotApi api)
            : base(
                  SharedTransport.Rest,
                  api.Exchange,
                  [TradingMode.Spot],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetAssetOptions,
                GetAllAssetsOptions,
                GetBalancesOptions,
                GetBookTickerOptions,
                GetDepositHistoryOptions,
                GetFeeOptions,
                GetKlinesOptions,
                GetOrderBookOptions,
                GetRecentTradesOptions,
                GetWithdrawalHistoryOptions,
                GetSpotSymbolsOptions,
                GetSpotTickerOptions,
                GetAllSpotTickersOptions,
                PlaceSpotOrderOptions,
                GetSpotOrderOptions,
                GetOpenSpotOrdersOptions,
                GetClosedSpotOrdersOptions,
                GetSpotOrderTradesOptions,
                GetSpotUserTradeHistoryOptions,
                CancelSpotOrderOptions,
                GetSpotOrderByClientOrderIdOptions,
                CancelSpotOrderByClientOrderIdOptions
                );
        }

    }
}
