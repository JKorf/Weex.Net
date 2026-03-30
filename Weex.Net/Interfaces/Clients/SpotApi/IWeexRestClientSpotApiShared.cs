using CryptoExchange.Net.SharedApis;

namespace Weex.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot rest API usage
    /// </summary>
    public interface IWeexRestClientSpotApiShared :
        IAssetsRestClient,
        IBalanceRestClient,
        IBookTickerRestClient,
        IDepositRestClient,
        IFeeRestClient,
        IKlineRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        IWithdrawalRestClient,
        ISpotSymbolRestClient,
        ISpotTickerRestClient,
        ISpotOrderRestClient,
        ISpotOrderClientIdRestClient
    {
    }
}
