using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Sessions.Interfaces;

namespace Weex.Net.Interfaces;

/// <summary>
/// User-bound spot order operations supported by Weex
/// </summary>
public interface IWeexSpotTradingUser :
    ITradingUserApi,
    ICancelSpotOrderOperation,
    ICancelSpotOrderByClientOrderIdOperation,
    IGetClosedSpotOrdersOperation,
    IGetOpenSpotOrdersOperation,
    IGetSpotOrderOperation,
    IGetSpotOrderByClientOrderIdOperation,
    IPlaceSpotOrderOperation
{
}
