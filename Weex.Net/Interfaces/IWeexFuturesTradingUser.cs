using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Sessions.Interfaces;

namespace Weex.Net.Interfaces;

/// <summary>
/// User-bound futures order operations supported by Weex
/// </summary>
public interface IWeexFuturesTradingUser :
    ITradingUserApi,
    ICancelFuturesOrderOperation,
    IGetClosedFuturesOrdersOperation,
    IGetFuturesOrderOperation,
    IGetOpenFuturesOrdersOperation,
    IPlaceFuturesOrderOperation
{
}
