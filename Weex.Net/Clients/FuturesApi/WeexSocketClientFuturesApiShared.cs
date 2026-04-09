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
    internal partial class WeexSocketClientFuturesApi : IWeexSocketClientFuturesApiShared
    {
        private const string _topicId = "WeexFutures";
        public string Exchange => "Weex";

        public TradingMode[] SupportedTradingModes => new[] { TradingMode.PerpetualLinear };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Balance client
        EndpointOptions<SubscribeBalancesRequest> IBalanceSocketClient.SubscribeBalanceOptions { get; } = new EndpointOptions<SubscribeBalancesRequest>(true);
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToAccountUpdatesAsync(
                update => handler(update.ToType(update.Data.Balances.Select(x => new SharedBalance(x.Asset, x.Quantity, x.LegacyQuantity)).ToArray())),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.TwoHours,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.SixHours,
            SharedKlineInterval.EightHours,
            SharedKlineInterval.TwelveHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth)
        {
            SupportsMultipleSymbols = true
        };
        async Task<ExchangeResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeResult<UpdateSubscription>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToKlineUpdatesAsync(symbols, interval, update =>
            {
                if (update.UpdateType == SocketUpdateType.Snapshot)
                    return;

                var kline = update.Data.Single();
                handler(update.ToType(
                    new SharedKline(
                        ExchangeSymbolCache.ParseSymbol(_topicId, update.Symbol),
                        update.Symbol!,
                        kline.OpenTime,
                        kline.ClosePrice,
                        kline.HighPrice,
                        kline.LowPrice,
                        kline.OpenPrice,
                        kline.Volume)));
            }, ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Ticker client
        SubscribeTickerOptions ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscribeTickerOptions()
        {
            SupportsMultipleSymbols = true
        };
        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToTickerUpdatesAsync(symbols, update => handler(update.ToType(
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, update.Symbol),
                    update.Symbol!,
                    update.Data.LastPrice,
                    update.Data.HighPrice,
                    update.Data.LowPrice,
                    update.Data.Volume,
                    update.Data.PriceChangePercentage * 100)
                {
                    QuoteVolume = update.Data.QuoteVolume
                })), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Trade client

        EndpointOptions<SubscribeTradeRequest> ITradeSocketClient.SubscribeTradeOptions { get; } = new EndpointOptions<SubscribeTradeRequest>(false)
        {
            SupportsMultipleSymbols = true
        };
        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToTradeUpdatesAsync(symbols, update =>
            {
                if (update.UpdateType == SocketUpdateType.Snapshot)
                    return;

                handler(update.ToType(update.Data.Select(x =>
                    new SharedTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, update.Symbol),
                        update.Symbol!,
                        x.Quantity,
                        x.Price,
                        x.Timestamp)
                    {
                        Side = x.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy
                    }).ToArray()));
            }, ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region User Trade client

        EndpointOptions<SubscribeUserTradeRequest> IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new EndpointOptions<SubscribeUserTradeRequest>(true);
        async Task<ExchangeResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = ((IUserTradeSocketClient)this).SubscribeUserTradeOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToUserTradeUpdatesAsync(
                update =>
                {
                    handler(update.ToType(update.Data.Trades.Select(x =>
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol),
                            x.Symbol,
                            x.OrderId.ToString(),
                            x.Id,
                            x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            x.FillQuantity,
                            Math.Round(x.FillValue / x.FillQuantity, 8),
                            x.CreateTime)
                        {
                            Fee = x.FillFee,
                            Role = x.Direction == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
                        }
                    ).ToArray()));
                },
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Futures Order client

        EndpointOptions<SubscribeFuturesOrderRequest> IFuturesOrderSocketClient.SubscribeFuturesOrderOptions { get; } = new EndpointOptions<SubscribeFuturesOrderRequest>(true);
        async Task<ExchangeResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderSocketClient)this).SubscribeFuturesOrderOptions.ValidateRequest(Exchange, request, null, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToOrderUpdatesAsync(
                update => handler(update.ToType(update.Data.Orders.Select(x =>
                    new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol),
                        x.Symbol,
                        x.Id.ToString(),
                        ParseOrderType(x.OrderType),
                        x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(x.Status),
                        x.CreateTime)
                    {
                        ClientOrderId = x.ClientOrderId,
                        OrderPrice = x.Price == 0 ? null : x.Price,
                        OrderQuantity = new SharedOrderQuantity(x.Quantity),
                        QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.ValueFilled),
                        UpdateTime = x.UpdateTime,
                        Fee = x.TotalFillFee,
                        TimeInForce = ParseTimeInForce(x.TimeInForce),
                        AveragePrice = x.QuantityFilled > 0 ? Math.Round(x.ValueFilled / x.QuantityFilled, 8) : null,
                        PositionSide = x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                        ReduceOnly = x.ReduceOnly
                    }
                ).ToArray())),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        private SharedTimeInForce? ParseTimeInForce(TimeInForce timeInForce)
        {
            if (timeInForce == TimeInForce.GoodTillCanceled)
                return SharedTimeInForce.GoodTillCanceled;
            if (timeInForce == TimeInForce.FillOrKill)
                return SharedTimeInForce.FillOrKill;
            if (timeInForce == TimeInForce.ImmediateOrCancel)
                return SharedTimeInForce.ImmediateOrCancel;

            return null;
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == Enums.OrderStatus.Canceled || status == OrderStatus.Expired)
                return SharedOrderStatus.Canceled;
            if (status == Enums.OrderStatus.New || status == Enums.OrderStatus.Pending || status == Enums.OrderStatus.Canceling || status == OrderStatus.Untriggered)
                return SharedOrderStatus.Open;
            if (status == OrderStatus.Filled)
                return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }

        private SharedOrderType ParseOrderType(FuturesOrderType type)
        {
            if (type == FuturesOrderType.Market)
                return SharedOrderType.Market;

            if (type == FuturesOrderType.Limit)
                return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }
        #endregion

        #region Position client
        EndpointOptions<SubscribePositionRequest> IPositionSocketClient.SubscribePositionOptions { get; } = new EndpointOptions<SubscribePositionRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IPositionSocketClient.SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = ((IPositionSocketClient)this).SubscribePositionOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToPositionUpdatesAsync(
                update => handler(update.ToType(update.Data.Positions.Select(x => 
                new SharedPosition(
                    ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol),
                    x.Symbol,
                    x.Quantity,
                    x.UpdateTime)
                {
                    PositionMode = SharedPositionMode.HedgeMode,
                    PositionSide = x.Side == Enums.PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                    Leverage = x.Leverage
                }).ToArray())),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion
    }
}
