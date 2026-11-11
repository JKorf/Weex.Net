using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Sessions;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Interfaces;
using Weex.Net.Interfaces.Clients;

namespace Weex.Net;

internal sealed class WeexSpotTradingUser : TradingSessionUserApi, IWeexSpotTradingUser
{
    internal WeexSpotTradingUser(
        string userIdentifier,
        WeexEnvironment? environment,
        IWeexUserClientProvider provider)
        : base(userIdentifier, environment, provider, client =>
        {
            var weex = (IWeexSharedApiClient)client;
            return new ISharedApi[] { weex.SpotRest, weex.SpotSocket };
        })
    {
        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.CancelOrder);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.CancelOrderByClientOrderId);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.GetClosedOrders);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.GetOpenOrders);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.GetOrder);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.GetOrderByClientOrderId);
        GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.PlaceOrder);
    }

    CancelSpotOrderOptions ICancelSpotOrderOperation.CancelSpotOrderOptions
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.CancelOrder).CancelSpotOrderOptions;

    Task<IExchangeCallResult<SharedId>> ICancelSpotOrderOperation.CancelSpotOrderAsync(
        CancelOrderRequest request,
        CancellationToken ct)
        => base.CancelSpotOrderAsync(request, ct);

    CancelSpotOrderByClientOrderIdOptions ICancelSpotOrderByClientOrderIdOperation.CancelSpotOrderByClientOrderIdOptions
        => GetRequiredPublicCapability(
            SharedCapabilities.Orders.Spot.CancelOrderByClientOrderId).CancelSpotOrderByClientOrderIdOptions;

    Task<IExchangeCallResult<SharedId>> ICancelSpotOrderByClientOrderIdOperation.CancelSpotOrderByClientOrderIdAsync(
        CancelOrderRequest request,
        CancellationToken ct)
        => base.CancelSpotOrderByClientOrderIdAsync(request, ct);

    GetSpotClosedOrdersOptions IGetClosedSpotOrdersOperation.GetClosedSpotOrdersOptions
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.GetClosedOrders).GetClosedSpotOrdersOptions;

    Task<IExchangeCallResult<SharedSpotOrder[]>> IGetClosedSpotOrdersOperation.GetClosedSpotOrdersAsync(
        GetClosedOrdersRequest request,
        PageRequest? nextPageToken,
        CancellationToken ct)
        => base.GetClosedSpotOrdersAsync(request, nextPageToken, ct);

    GetOpenSpotOrdersOptions IGetOpenSpotOrdersOperation.GetOpenSpotOrdersOptions
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.GetOpenOrders).GetOpenSpotOrdersOptions;

    Task<IExchangeCallResult<SharedSpotOrder[]>> IGetOpenSpotOrdersOperation.GetOpenSpotOrdersAsync(
        GetOpenOrdersRequest request,
        CancellationToken ct)
        => base.GetOpenSpotOrdersAsync(request, ct);

    GetSpotOrderOptions IGetSpotOrderOperation.GetSpotOrderOptions
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.GetOrder).GetSpotOrderOptions;

    Task<IExchangeCallResult<SharedSpotOrder>> IGetSpotOrderOperation.GetSpotOrderAsync(
        GetOrderRequest request,
        CancellationToken ct)
        => base.GetSpotOrderAsync(request, ct);

    GetSpotOrderByClientOrderIdOptions IGetSpotOrderByClientOrderIdOperation.GetSpotOrderByClientOrderIdOptions
        => GetRequiredPublicCapability(
            SharedCapabilities.Orders.Spot.GetOrderByClientOrderId).GetSpotOrderByClientOrderIdOptions;

    Task<IExchangeCallResult<SharedSpotOrder>> IGetSpotOrderByClientOrderIdOperation.GetSpotOrderByClientOrderIdAsync(
        GetOrderRequest request,
        CancellationToken ct)
        => base.GetSpotOrderByClientOrderIdAsync(request, ct);

    PlaceSpotOrderOptions IPlaceSpotOrderOperation.PlaceSpotOrderOptions
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.PlaceOrder).PlaceSpotOrderOptions;

    Task<IExchangeCallResult<SharedId>> IPlaceSpotOrderOperation.PlaceSpotOrderAsync(
        PlaceSpotOrderRequest request,
        CancellationToken ct)
        => base.PlaceSpotOrderAsync(request, ct);

    SharedFeeDeductionType ISpotOrderPlacementMetadata.SpotFeeDeductionType
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.PlaceOrder).SpotFeeDeductionType;

    SharedFeeAssetType ISpotOrderPlacementMetadata.SpotFeeAssetType
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.PlaceOrder).SpotFeeAssetType;

    SharedOrderType[] ISpotOrderPlacementMetadata.SpotSupportedOrderTypes
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.PlaceOrder).SpotSupportedOrderTypes;

    SharedTimeInForce[] ISpotOrderPlacementMetadata.SpotSupportedTimeInForce
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.PlaceOrder).SpotSupportedTimeInForce;

    SharedQuantitySupport ISpotOrderPlacementMetadata.SpotSupportedOrderQuantity
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.PlaceOrder).SpotSupportedOrderQuantity;

    string IClientOrderIdGenerator.GenerateClientOrderId()
        => GetRequiredPublicCapability(SharedCapabilities.Orders.Spot.PlaceOrder).GenerateClientOrderId();
}
