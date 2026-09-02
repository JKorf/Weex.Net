using CryptoExchange.Net.SharedApis;

namespace Weex.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Shared interface for Futures socket API usage
    /// </summary>
    public interface IWeexSocketClientFuturesApiShared :
        IBalanceSocketClient,
        IKlineSocketClient,
        ITickerSocketClient,
        ITradeSocketClient,
        IUserTradeSocketClient,
        IFuturesOrderSocketClient,
        IPositionSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IWeexSocketClientFuturesSharedApi :
        ISubscribeBalancesSocket,
        ISubscribeKlinesSocket,
        ISubscribeTickerSocket,
        ISubscribeTradesSocket,
        ISubscribeUserTradesSocket,
        ISubscribeFuturesOrdersSocket,
        ISubscribePositionsSocket
    {
    }
}
