using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Objects.Models;

namespace Weex.Net.Clients.FuturesApi
{
    internal partial class WeexRestClientFuturesSharedApi
    {
        #region Leverage client
        public SharedLeverageSettingMode LeverageSettingType => SharedLeverageSettingMode.PerSymbol;

        public GetLeverageOptions GetLeverageOptions { get; } = new GetLeverageOptions(_exchangeName, true);
        public async Task<HttpResult<SharedLeverage>> GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = GetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.GetSymbolConfigAsync(symbol: request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            var symbolConfig = result.Data.Single();
            return HttpResult.Ok(result, new SharedLeverage(
                    request.MarginMode == SharedMarginMode.Isolated ? symbolConfig.IsolatedLongLeverage : symbolConfig.CrossLeverage));
        }

        public SetLeverageOptions SetLeverageOptions { get; } = new SetLeverageOptions(_exchangeName)
        {
            RequiredRequestParameters = [
                RequestParameter<SetLeverageRequest>.Required(x => x.MarginMode,"Margin mode to adjust leverage for", SharedMarginMode.Cross)
                ]
        };
        public async Task<HttpResult<SharedLeverage>> SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = SetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var marginMode = request.MarginMode == SharedMarginMode.Cross ? MarginType.Cross : MarginType.Isolated;
            var result = await _api.Account.SetLeverageAsync(
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                marginMode,
                marginMode == MarginType.Cross ? request.Leverage : null, 
                marginMode == MarginType.Isolated ? request.Leverage : null,
                marginMode == MarginType.Isolated ? request.Leverage : null,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            return HttpResult.Ok(result, new SharedLeverage(
                    request.MarginMode == SharedMarginMode.Isolated ? result.Data.IsolatedLongLeverage : result.Data.CrossLeverage));
        }
        #endregion
    }
}
