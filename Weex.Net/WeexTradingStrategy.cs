using CryptoExchange.Net.TradeSessions;
using Weex.Net.Interfaces;

namespace Weex.Net;

/// <summary>
/// Weex trading strategy
/// </summary>
public abstract class WeexTradingStrategy : TradeSession<IWeexTradingUser>
{
}
