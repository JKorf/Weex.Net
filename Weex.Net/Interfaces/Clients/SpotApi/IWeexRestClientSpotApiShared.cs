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

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IWeexRestClientSpotSharedApi :
        IGetAllAssetsRest,
        IGetAssetRest,
        IGetBalancesRest,
        IGetBookTickerRest,
        IGetDepositHistoryRest,
        IGetFeesRest,
        IGetKlinesRest,
        IGetOrderBookRest,
        IGetRecentTradesRest,
        IGetWithdrawalHistoryRest,
        IGetSpotSymbolsRest,
        IGetSpotTickerRest,
        IGetAllSpotTickersRest,
        IPlaceSpotOrderRest,
        ICancelSpotOrderRest,
        IGetSpotOrderRest,
        IGetOpenSpotOrdersRest,
        IGetClosedSpotOrdersRest,
        IGetSpotOrderTradesRest,
        IGetSpotUserTradeHistoryRest,
        IGetSpotOrderByClientOrderIdRest,
        ICancelSpotOrderByClientOrderIdRest
    {
    }
}
