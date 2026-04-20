using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Weex.Net.Enums;
using Weex.Net.Objects.Models;

namespace Weex.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Weex Futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IWeexRestClientFuturesApiExchangeData
    {
        /// <summary>
        /// Get server time
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/ConfigAPI/GetServerTime" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/time<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get asset and symbol info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetContractInfo" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/exchangeInfo<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesExchangeInfo>> GetExchangeInfoAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get order book snapshot
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetDepthData" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/depth<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="depth">["<c>limit</c>"] Book depth, either 15 or 200</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexOrderBook>> GetOrderBookAsync(string symbol, int? depth = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24h ticker price statistics
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetTicker24h" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/ticker/24hr<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesTicker[]>> GetTickersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get best price/quantity on the order book for a symbol or symbols.
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetBookTicker" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/ticker/bookTicker<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesBookTicker[]>> GetBookTickersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get recent trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetRecentTrades" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/trades<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get klines/candles for a symbol.
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetKlines" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/klines<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexKline[]>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get index price klines/candles for a symbol.
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetIndexPriceKlines" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/indexPriceKlines<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexKline[]>> GetIndexPriceKlinesAsync(string symbol, FuturesKlineInterval interval, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get mark price klines/candles for a symbol.
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetMarkPriceKlines" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/markPriceKlines<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexKline[]>> GetMarkPriceKlinesAsync(string symbol, FuturesKlineInterval interval, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get kline history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetHistoryKlines" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/historyKlines<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 100</param>
        /// <param name="priceType">["<c>priceType</c>"] Price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexKline[]>> GetKlineHistoryAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, PriceType? priceType = null, CancellationToken ct = default);

        /// <summary>
        /// Get index/mark price for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetSymbolPrice" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/symbolPrice<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="priceType">["<c>priceType</c>"] Price type, defaults to Index</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesPrice>> GetPriceAsync(string symbol, PriceType? priceType = null, CancellationToken ct = default);

        /// <summary>
        /// Get open interest for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetOpenInterest" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/openInterest<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get current funding rate info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetCurrentFundingRate" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/premiumIndex<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFundingInfo[]>> GetFundingRateAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetFundingRateHistory" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/fundingRate<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFundingHistory[]>> GetFundingRateHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Market_API/GetApiTradingSymbols" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/market/apiTradingSymbols<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<string[]>> GetTradingSymbolsAsync(CancellationToken ct = default);

    }
}
