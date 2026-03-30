using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Objects.Internal;
using Weex.Net.Objects.Models;

namespace Weex.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Weex Futures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IWeexRestClientFuturesApiAccount
    {
        /// <summary>
        /// Get account balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Account_API/GetAccountBalance" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/account/balance<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesBalance[]>> GetBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get trading fees for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Account_API/GetCommissionRate" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/account/commissionRate<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexTradingFee>> GetTradingFeesAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get account configuration
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Account_API/GetAccountConfig" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/account/accountConfig<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexAccountConfig>> GetAccountConfigAsync(CancellationToken ct = default);

        /// <summary>
        /// Get symbol configuration
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Account_API/GetSymbolConfig" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/account/symbolConfig<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexSymbolConfig[]>> GetSymbolConfigAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get account income bills
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Account_API/GetContractBills" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/account/income<br />
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="incomeType">["<c>incomeType</c>"] Filter by income type</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesBillPage>> GetAccountBillsAsync(string? asset = null, string? symbol = null, IncomeType? incomeType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Set margin mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Account_API/ChangeMarginModeTRADE" /><br />
        /// Endpoint:<br />
        /// POST /capi/v3/account/marginType<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="marginType">["<c>marginType</c>"] New margin mode</param>
        /// <param name="combineType">["<c>separatedType</c>"] New combine type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetMarginModeAsync(string symbol, MarginType marginType, PositionCombineType? combineType = null, CancellationToken ct = default);

        /// <summary>
        /// Set leverage configuration
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Account_API/UpdateLeverageTRADE" /><br />
        /// Endpoint:<br />
        /// POST /capi/v3/account/leverage<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="marginMode">["<c>marginType</c>"] Target margin mode</param>
        /// <param name="crossLeverage">["<c>crossLeverage</c>"] Cross margin leverage</param>
        /// <param name="isolatedLongLeverage">["<c>isolatedLongLeverage</c>"] Isolated margin long leverage</param>
        /// <param name="isolatedShortLeverage">["<c>isolatedShortLeverage</c>"] Isolated margin short leverage</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexLeverageResult>> SetLeverageAsync(string symbol, MarginType? marginMode = null, decimal? crossLeverage = null, decimal? isolatedLongLeverage = null, decimal? isolatedShortLeverage = null, CancellationToken ct = default);

        /// <summary>
        /// Adjust isolated margin for a position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Account_API/AdjustPositionMarginTRADE" /><br />
        /// Endpoint:<br />
        /// POST /capi/v3/account/positionMargin<br />
        /// </para>
        /// </summary>
        /// <param name="positionId">["<c>isolatedPositionId</c>"] The position id</param>
        /// <param name="quantity">["<c>amount</c>"] Adjust quantity</param>
        /// <param name="adjustType">["<c>type</c>"] Adjust type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> AdjustIsolatedMarginAsync(long positionId, decimal quantity, MarginAdjustType adjustType, CancellationToken ct = default);

        /// <summary>
        /// Set auto append margin setting for a position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Account_API/ModifyAutoAppendMarginTRADE" /><br />
        /// Endpoint:<br />
        /// POST /capi/v3/account/modifyAutoAppendMargin<br />
        /// </para>
        /// </summary>
        /// <param name="positionId">["<c>positionId</c>"] Id of the position</param>
        /// <param name="autoAppendMargin">["<c>autoAppendMargin</c>"] Auto append margin</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetAutoAppendMarginAsync(long positionId, bool autoAppendMargin, CancellationToken ct = default);

    }
}
