using CryptoExchange.Net.SharedApis;

namespace Weex.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot socket API usage
    /// </summary>
    public interface IWeexSocketClientSpotApiShared :
        IBalanceSocketClient,
        IBookTickerSocketClient,
        IKlineSocketClient,
        ITickerSocketClient,
        ITradeSocketClient,
        IUserTradeSocketClient,
        ISpotOrderSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IWeexSocketClientSpotSharedApi :
        ISubscribeBalancesOperation,
        ISubscribeBookTickerOperation,
        ISubscribeKlinesOperation,
        ISubscribeTickerOperation,
        ISubscribeTradesOperation,
        ISubscribeUserTradesOperation,
        ISubscribeSpotOrdersOperation
    {
    }
}
