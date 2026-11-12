using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.TradeSessions.Interfaces;

namespace Weex.Net.Interfaces;

/// <summary>
/// User-bound spot order operations supported by Weex
/// </summary>
public interface IWeexSpotTradingUser :
    ITradeSessionUserApi,
    ICancelSpotOrderOperation,
    ICancelSpotOrderByClientOrderIdOperation,
    IGetClosedSpotOrdersOperation,
    IGetOpenSpotOrdersOperation,
    IGetSpotOrderOperation,
    IGetSpotOrderByClientOrderIdOperation,
    IPlaceSpotOrderOperation
{
}
