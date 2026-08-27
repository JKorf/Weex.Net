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

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IWeexRestClientFuturesSharedApi :
        IGetBalancesEndpoint,
        IGetBookTickerEndpoint,
        IGetFeesEndpoint,
        IGetKlinesEndpoint,
        IGetOrderBookEndpoint,
        IGetRecentTradesEndpoint,
        IGetFundingRateHistoryEndpoint,
        IGetFuturesSymbolsEndpoint,
        IGetFuturesTickerEndpoint,
        IGetAllFuturesTickersEndpoint,
        IGetIndexPriceKlinesEndpoint,
        IGetMarkPriceKlinesEndpoint,
        IGetLeverageEndpoint,
        ISetLeverageEndpoint,
        IGetOpenInterestEndpoint,
        IPlaceFuturesOrderEndpoint,
        IGetFuturesOrderEndpoint,
        IGetOpenFuturesOrdersEndpoint,
        IGetClosedFuturesOrdersEndpoint,
        IGetFuturesOrderTradesEndpoint,
        IGetFuturesUserTradeHistoryEndpoint,
        ICancelFuturesOrderEndpoint,
        IGetPositionsEndpoint,
        IClosePositionEndpoint,
        IPlaceFuturesTriggerOrderEndpoint,
        IGetFuturesTriggerOrderEndpoint,
        ICancelFuturesTriggerOrderEndpoint
    {
    }
}
