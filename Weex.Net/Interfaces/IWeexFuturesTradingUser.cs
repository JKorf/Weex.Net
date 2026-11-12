using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.TradeSessions.Interfaces;

namespace Weex.Net.Interfaces;

/// <summary>
/// User-bound futures order operations supported by Weex
/// </summary>
public interface IWeexFuturesTradingUser :
    ITradeSessionUserApi,
    ICancelFuturesOrderOperation,
    IGetClosedFuturesOrdersOperation,
    IGetFuturesOrderOperation,
    IGetOpenFuturesOrdersOperation,
    IPlaceFuturesOrderOperation
{
}
