using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Sessions;
using CryptoExchange.Net.Trackers.Sessions.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Interfaces;
using Weex.Net.Interfaces.Clients;

namespace Weex.Net;

/// <summary>
/// Weex trading session user
/// </summary>
internal sealed class WeexSessionUser : TradingSessionUser, IWeexTradingUser
{
    internal WeexSessionUser(TradingSessionUserContext context, IWeexUserClientProvider provider)
        : base(context, provider)
    {
        if (Environment != null && Environment is not WeexEnvironment)
            throw new ArgumentException("A WeexEnvironment is required.", nameof(context));

        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.CancelOrder, TradingMode.Spot);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.CancelOrderByClientOrderId, TradingMode.Spot);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.GetClosedOrders, TradingMode.Spot);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.GetOpenOrders, TradingMode.Spot);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.GetOrder, TradingMode.Spot);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.GetOrderByClientOrderId, TradingMode.Spot);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.PlaceOrder, TradingMode.Spot);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.CancelOrder, TradingMode.PerpetualLinear);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.GetClosedOrders, TradingMode.PerpetualLinear);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.GetOrder, TradingMode.PerpetualLinear);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.GetOpenOrders, TradingMode.PerpetualLinear);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.PlaceOrder, TradingMode.PerpetualLinear);
    }

    CancelSpotOrderOptions ITradingUserCancelSpotOrder.CancelSpotOrderOptions
        => base.CancelSpotOrderOptions
            ?? throw new NotSupportedException("The order operation does not support this trading mode.");

    Task<IExchangeCallResult<SharedId>> ITradingUserCancelSpotOrder.CancelSpotOrderAsync(
        CancelOrderRequest request,
        CancellationToken ct)
        => base.CancelSpotOrderAsync(request, ct);

    CancelSpotOrderByClientOrderIdOptions
        ITradingUserCancelSpotOrderByClientOrderId.CancelSpotOrderByClientOrderIdOptions
        => base.CancelSpotOrderByClientOrderIdOptions
            ?? throw new NotSupportedException("The order operation does not support this trading mode.");

    Task<IExchangeCallResult<SharedId>> ITradingUserCancelSpotOrderByClientOrderId.CancelSpotOrderByClientOrderIdAsync(
        CancelOrderRequest request,
        CancellationToken ct)
        => base.CancelSpotOrderByClientOrderIdAsync(request, ct);

    GetSpotClosedOrdersOptions ITradingUserGetClosedSpotOrders.GetClosedSpotOrdersOptions
        => base.GetClosedSpotOrdersOptions
            ?? throw new NotSupportedException("The order operation does not support this trading mode.");

    Task<IExchangeCallResult<SharedSpotOrder[]>> ITradingUserGetClosedSpotOrders.GetClosedSpotOrdersAsync(
        GetClosedOrdersRequest request,
        PageRequest? nextPageToken,
        CancellationToken ct)
        => base.GetClosedSpotOrdersAsync(request, nextPageToken, ct);

    GetOpenSpotOrdersOptions ITradingUserGetOpenSpotOrders.GetOpenSpotOrdersOptions
        => base.GetOpenSpotOrdersOptions
            ?? throw new NotSupportedException("The order operation does not support this trading mode.");

    Task<IExchangeCallResult<SharedSpotOrder[]>> ITradingUserGetOpenSpotOrders.GetOpenSpotOrdersAsync(
        GetOpenOrdersRequest request,
        CancellationToken ct)
        => base.GetOpenSpotOrdersAsync(request, ct);

    GetSpotOrderOptions ITradingUserGetSpotOrder.GetSpotOrderOptions
        => base.GetSpotOrderOptions
            ?? throw new NotSupportedException("The order operation does not support this trading mode.");

    Task<IExchangeCallResult<SharedSpotOrder>> ITradingUserGetSpotOrder.GetSpotOrderAsync(
        GetOrderRequest request,
        CancellationToken ct)
        => base.GetSpotOrderAsync(request, ct);

    GetSpotOrderByClientOrderIdOptions ITradingUserGetSpotOrderByClientOrderId.GetSpotOrderByClientOrderIdOptions
        => base.GetSpotOrderByClientOrderIdOptions
            ?? throw new NotSupportedException("The order operation does not support this trading mode.");

    Task<IExchangeCallResult<SharedSpotOrder>> ITradingUserGetSpotOrderByClientOrderId.GetSpotOrderByClientOrderIdAsync(
        GetOrderRequest request,
        CancellationToken ct)
        => base.GetSpotOrderByClientOrderIdAsync(request, ct);

    PlaceSpotOrderOptions ITradingUserPlaceSpotOrder.PlaceSpotOrderOptions
        => base.PlaceSpotOrderOptions
            ?? throw new NotSupportedException("The order operation does not support this trading mode.");

    Task<IExchangeCallResult<SharedId>> ITradingUserPlaceSpotOrder.PlaceSpotOrderAsync(
        PlaceSpotOrderRequest request,
        CancellationToken ct)
        => base.PlaceSpotOrderAsync(request, ct);

    CancelFuturesOrderOptions ITradingUserCancelFuturesOrder.GetCancelFuturesOrderOptions(TradingMode tradingMode)
        => base.GetCancelFuturesOrderOptions(tradingMode)
            ?? throw new NotSupportedException("The order operation does not support this trading mode.");

    Task<IExchangeCallResult<SharedId>> ITradingUserCancelFuturesOrder.CancelFuturesOrderAsync(
        CancelOrderRequest request,
        CancellationToken ct)
        => base.CancelFuturesOrderAsync(request, ct);

    GetFuturesClosedOrdersOptions ITradingUserGetClosedFuturesOrders.GetClosedFuturesOrdersOptions(
        TradingMode tradingMode)
        => base.GetClosedFuturesOrdersOptions(tradingMode)
            ?? throw new NotSupportedException("The order operation does not support this trading mode.");

    Task<IExchangeCallResult<SharedFuturesOrder[]>> ITradingUserGetClosedFuturesOrders.GetClosedFuturesOrdersAsync(
        GetClosedOrdersRequest request,
        PageRequest? nextPageToken,
        CancellationToken ct)
        => base.GetClosedFuturesOrdersAsync(request, nextPageToken, ct);

    GetFuturesOrderOptions ITradingUserGetFuturesOrder.GetFuturesOrderOptions(TradingMode tradingMode)
        => base.GetFuturesOrderOptions(tradingMode)
            ?? throw new NotSupportedException("The order operation does not support this trading mode.");

    Task<IExchangeCallResult<SharedFuturesOrder>> ITradingUserGetFuturesOrder.GetFuturesOrderAsync(
        GetOrderRequest request,
        CancellationToken ct)
        => base.GetFuturesOrderAsync(request, ct);

    GetOpenFuturesOrdersOptions ITradingUserGetOpenFuturesOrders.GetOpenFuturesOrdersOptions(TradingMode tradingMode)
        => base.GetOpenFuturesOrdersOptions(tradingMode)
            ?? throw new NotSupportedException("The order operation does not support this trading mode.");

    Task<IExchangeCallResult<SharedFuturesOrder[]>> ITradingUserGetOpenFuturesOrders.GetOpenFuturesOrdersAsync(
        GetOpenOrdersRequest request,
        CancellationToken ct)
        => base.GetOpenFuturesOrdersAsync(request, ct);

    PlaceFuturesOrderOptions ITradingUserPlaceFuturesOrder.GetPlaceFuturesOrderOptions(TradingMode tradingMode)
        => base.GetPlaceFuturesOrderOptions(tradingMode)
            ?? throw new NotSupportedException("The order operation does not support this trading mode.");

    Task<IExchangeCallResult<SharedId>> ITradingUserPlaceFuturesOrder.PlaceFuturesOrderAsync(
        PlaceFuturesOrderRequest request,
        CancellationToken ct)
        => base.PlaceFuturesOrderAsync(request, ct);
}
