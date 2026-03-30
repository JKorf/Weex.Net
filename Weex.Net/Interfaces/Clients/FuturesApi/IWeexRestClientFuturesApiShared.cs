using CryptoExchange.Net.SharedApis;

namespace Weex.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Shared interface for Futures rest API usage
    /// </summary>
    public interface IWeexRestClientFuturesApiShared :
        IBalanceRestClient,
        IBookTickerRestClient,
        IFeeRestClient,
        IKlineRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        IFundingRateRestClient,
        IFuturesSymbolRestClient,
        IFuturesTickerRestClient,
        IIndexPriceKlineRestClient,
        IMarkPriceKlineRestClient,
        ILeverageRestClient,
        IOpenInterestRestClient,
        IFuturesOrderRestClient,
        IFuturesTriggerOrderRestClient
    {
    }
}
