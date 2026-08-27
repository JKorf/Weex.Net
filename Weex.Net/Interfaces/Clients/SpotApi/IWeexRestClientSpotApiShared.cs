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
        IGetAllAssetsEndpoint,
        IGetAssetEndpoint,
        IGetBalancesEndpoint,
        IGetBookTickerEndpoint,
        IGetDepositHistoryEndpoint,
        IGetFeesEndpoint,
        IGetKlinesEndpoint,
        IGetOrderBookEndpoint,
        IGetRecentTradesEndpoint,
        IGetWithdrawalHistoryEndpoint,
        IGetSpotSymbolsEndpoint,
        IGetSpotTickerEndpoint,
        IGetAllSpotTickersEndpoint,
        IPlaceSpotOrderEndpoint,
        ICancelSpotOrderEndpoint,
        IGetSpotOrderEndpoint,
        IGetOpenSpotOrdersEndpoint,
        IGetClosedSpotOrdersEndpoint,
        IGetSpotOrderTradesEndpoint,
        IGetSpotUserTradeHistoryEndpoint,
        IGetSpotOrderByClientOrderIdEndpoint,
        ICancelSpotOrderByClientOrderIdEndpoint
    {
    }
}
