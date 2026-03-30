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
}
