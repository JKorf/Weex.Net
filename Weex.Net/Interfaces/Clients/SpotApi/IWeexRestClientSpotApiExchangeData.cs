using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Weex.Net.Enums;
using Weex.Net.Objects.Models;

namespace Weex.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Weex Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IWeexRestClientSpotApiExchangeData
    {
        /// <summary>
        /// 
        /// <para><a href="XXX" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get list of support assets and network info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/ConfigAPI/CurrencyInfo" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/coins<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexAsset[]>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get exchange information
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/ConfigAPI/GetProductInfo" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/exchangeInfo<br />
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] Filter by symbols, for example `ETHUSDT`</param>
        /// <param name="symbolStatus">["<c>symbolStatus</c>"] Filter by symbol status</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string>? symbols = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Get last price for symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/MarketDataAPI/GetTickerInfo" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/market/ticker/price<br />
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] Filter by symbols, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexPrice[]>> GetPricesAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24hr price change statistics for symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/MarketDataAPI/GetAllTickerInfo" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/market/ticker/24hr<br />
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] Filter symbols, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexTicker[]>> GetTickersAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get recent trades for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/MarketDataAPI/GetTradeData" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/market/trades<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/MarketDataAPI/GetKLineData" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/market/klines<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, CancellationToken ct = default);

        /// <summary>
        /// Get order book snapshot
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/MarketDataAPI/GetDepthData" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/market/depth<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] 15 or 200</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get book ticker (best bid/ask)
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/MarketDataAPI/GetBookTicker" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/market/ticker/bookTicker<br />
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexBookTicker[]>> GetBookTickersAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

    }
}
