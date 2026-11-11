using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Sessions;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Interfaces;
using Weex.Net.Interfaces.Clients;

namespace Weex.Net;

internal sealed class WeexFuturesTradingUser : TradingSessionUserApi, IWeexFuturesTradingUser
{
    internal WeexFuturesTradingUser(
        string userIdentifier,
        WeexEnvironment? environment,
        IWeexUserClientProvider provider)
        : base(userIdentifier, environment, provider, client =>
        {
            var weex = (IWeexSharedApiClient)client;
            return new ISharedApi[] { weex.FuturesRest, weex.FuturesSocket };
        })
    {
        GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.CancelOrder);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.GetClosedOrders);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.GetOrder);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.GetOpenOrders);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.PlaceOrder);
    }

    CancelFuturesOrderOptions ICancelFuturesOrderOperation.CancelFuturesOrderOptions
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.CancelOrder).CancelFuturesOrderOptions;

    Task<IExchangeCallResult<SharedId>> ICancelFuturesOrderOperation.CancelFuturesOrderAsync(
        CancelOrderRequest request,
        CancellationToken ct)
        => base.CancelFuturesOrderAsync(request, ct);

    GetFuturesClosedOrdersOptions IGetClosedFuturesOrdersOperation.GetClosedFuturesOrdersOptions
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.GetClosedOrders).GetClosedFuturesOrdersOptions;

    Task<IExchangeCallResult<SharedFuturesOrder[]>> IGetClosedFuturesOrdersOperation.GetClosedFuturesOrdersAsync(
        GetClosedOrdersRequest request,
        PageRequest? nextPageToken,
        CancellationToken ct)
        => base.GetClosedFuturesOrdersAsync(request, nextPageToken, ct);

    GetFuturesOrderOptions IGetFuturesOrderOperation.GetFuturesOrderOptions
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.GetOrder).GetFuturesOrderOptions;

    Task<IExchangeCallResult<SharedFuturesOrder>> IGetFuturesOrderOperation.GetFuturesOrderAsync(
        GetOrderRequest request,
        CancellationToken ct)
        => base.GetFuturesOrderAsync(request, ct);

    GetOpenFuturesOrdersOptions IGetOpenFuturesOrdersOperation.GetOpenFuturesOrdersOptions
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.GetOpenOrders).GetOpenFuturesOrdersOptions;

    Task<IExchangeCallResult<SharedFuturesOrder[]>> IGetOpenFuturesOrdersOperation.GetOpenFuturesOrdersAsync(
        GetOpenOrdersRequest request,
        CancellationToken ct)
        => base.GetOpenFuturesOrdersAsync(request, ct);

    PlaceFuturesOrderOptions IPlaceFuturesOrderOperation.PlaceFuturesOrderOptions
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.PlaceOrder).PlaceFuturesOrderOptions;

    Task<IExchangeCallResult<SharedId>> IPlaceFuturesOrderOperation.PlaceFuturesOrderAsync(
        PlaceFuturesOrderRequest request,
        CancellationToken ct)
        => base.PlaceFuturesOrderAsync(request, ct);

    SharedFeeDeductionType IFuturesOrderPlacementMetadata.FuturesFeeDeductionType
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.PlaceOrder).FuturesFeeDeductionType;

    SharedFeeAssetType IFuturesOrderPlacementMetadata.FuturesFeeAssetType
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.PlaceOrder).FuturesFeeAssetType;

    SharedOrderType[] IFuturesOrderPlacementMetadata.FuturesSupportedOrderTypes
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.PlaceOrder).FuturesSupportedOrderTypes;

    SharedTimeInForce[] IFuturesOrderPlacementMetadata.FuturesSupportedTimeInForce
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.PlaceOrder).FuturesSupportedTimeInForce;

    SharedQuantitySupport IFuturesOrderPlacementMetadata.FuturesSupportedOrderQuantity
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.PlaceOrder).FuturesSupportedOrderQuantity;

    string IClientOrderIdGenerator.GenerateClientOrderId()
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Futures.PlaceOrder).GenerateClientOrderId();
}
