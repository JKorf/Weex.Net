using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Interfaces.Clients.SpotApi;

namespace Weex.Net.Clients.SpotApi
{
    internal partial class WeexSocketClientSpotSharedApi
    {
        #region Subscribe Book Ticker

        public SubscribeBookTickerOptions SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToBookTickerUpdatesAsync(
                symbols, 
                update =>
                    handler(update.ToType(
                        new SharedBookTicker(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                            update.Data.Symbol,
                            update.Data.BestAskPrice,
                            new SharedOrderQuantity(update.Data.BestAskQuantity),
                            update.Data.BestBidPrice,
                            new SharedOrderQuantity(update.Data.BestBidQuantity))
                        )), ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
