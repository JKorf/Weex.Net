using CryptoExchange.Net.TradeSessions.Interfaces;

namespace Weex.Net.Interfaces;

/// <summary>
/// User access to the API surfaces supported by Weex
/// </summary>
public interface IWeexTradingUser : ITradingUser
{
    /// <summary>
    /// Spot order operations for this user
    /// </summary>
    IWeexSpotTradingUser Spot { get; }

    /// <summary>
    /// Futures order operations for this user
    /// </summary>
    IWeexFuturesTradingUser Futures { get; }
}
