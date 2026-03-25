using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Objects;
using Weex.Net.Objects.Models;

namespace Weex.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Weex Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IWeexRestClientSpotApiAccount
    {

        /// <summary>
        /// Get supported trading symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/ConfigAPI/GetAllProductInfo" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/apiTradingSymbols<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<string[]>> GetTradingSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get account info and balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/AccountAPI/GetAccountBalance" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/account<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get account bill history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/AccountAPI/GetBillRecords" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/account/bills<br />
        /// </para>
        /// </summary>
        /// <param name="assetId">["<c>coinId</c>"] Filter by asset id</param>
        /// <param name="businessType">["<c>bizType</c>"] Filter by business type</param>
        /// <param name="startTime">["<c>after</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>before</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexBill[]>> GetAccountBillsAsync(long? assetId = null, BusinessType? businessType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get account funding bills
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/AccountAPI/GetFundBillRecords" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/account/fundingBills<br />
        /// </para>
        /// </summary>
        /// <param name="assetId">["<c>coinId</c>"] Filter by asset id</param>
        /// <param name="businessType">["<c>bizType</c>"] Filter by business type</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>pageIndex</c>"] Page number</param>
        /// <param name="limit">["<c>pageSize</c>"] Max number of results, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexPage<WeexFundingBill>>> GetFundingBillsAsync(long? assetId = null, BusinessType? businessType = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get transfer history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/AccountAPI/TransferRecords" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/account/transferRecords<br />
        /// </para>
        /// </summary>
        /// <param name="assetId">["<c>coinId</c>"] Filter by asset id</param>
        /// <param name="fromType">["<c>fromType</c>"] Filter by from account type</param>
        /// <param name="startTime">["<c>after</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>before</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexTransfer[]>> GetTransferHistoryAsync(long? assetId = null, AccountType? fromType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

    }
}
