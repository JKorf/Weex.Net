using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Clients.FuturesApi;
using Weex.Net.Enums;
using Weex.Net.Interfaces.Clients.SpotApi;
using Weex.Net.Objects.Models;

namespace Weex.Net.Clients.SpotApi
{
    internal partial class WeexRestClientSpotSharedApi
    {
        #region Get Transfer History

        async Task<ICallResult<SharedTransfer[]>> IGetTransferHistory.GetTransferHistoryAsync(GetTransferHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetTransferHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetTransferHistoryOptions GetTransferHistoryOptions { get; } = new GetTransferHistoryOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedTransfer[]>> GetTransferHistoryAsync(GetTransferHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetTransferHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTransfer[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetTransferHistoryAsync(
                startTime: pageParams.StartTime ?? pageParams.EndTime?.AddDays(-3),
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTransfer[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Timestamp)),
                         result.Data.Length,
                         result.Data.Select(x => x.Timestamp),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                .Select(x =>
                    new SharedTransfer(
                        x.Asset,
                        x.Quantity,
                        ParseAccountType(x.FromType),
                        ParseAccountType(x.ToType),
                        x.Timestamp)
                    {
                    })
                .ToArray(), nextPageRequest);
        }

        private SharedAccountType ParseAccountType(AccountType? accountType)
        {
            if (accountType == AccountType.Spot)
                return SharedAccountType.Spot;
            if (accountType == AccountType.Funding)
                return SharedAccountType.Funding;

            return SharedAccountType.Other;
        }

        #endregion
    }
}
