using CryptoExchange.Net.Trackers.Sessions.Interfaces;

namespace Weex.Net.Interfaces;

/// <summary>
/// User access to the order operations supported by Weex
/// </summary>
public interface IWeexTradingUser :
    ITradingUser,
    ITradingUserCancelSpotOrder,
    ITradingUserCancelSpotOrderByClientOrderId,
    ITradingUserGetClosedSpotOrders,
    ITradingUserGetOpenSpotOrders,
    ITradingUserGetSpotOrder,
    ITradingUserGetSpotOrderByClientOrderId,
    ITradingUserPlaceSpotOrder,
    ITradingUserCancelFuturesOrder,
    ITradingUserGetClosedFuturesOrders,
    ITradingUserGetFuturesOrder,
    ITradingUserGetOpenFuturesOrders,
    ITradingUserPlaceFuturesOrder
{
}
