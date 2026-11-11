using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sessions;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Sessions;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Interfaces.Clients;

namespace Weex.Net
{
    /// <summary>
    /// Weex trading session
    /// </summary>
    public class WeexTradingSession : TradingSession<IWeexUserClientProvider, WeexSessionUser>
    {
        /// <inheritdoc />
        protected override WeexSessionUser CreateUser(TradingSessionUserDefinition definition)
        {
            return new WeexSessionUser(
                definition,
                ClientProvider,
                () => GetSpotUserDataTracker(definition.UserIdentifier),
                mode => GetFuturesUserDataTracker(
                    definition.UserIdentifier, mode));
        }
    }

    /// <summary>
    /// Weex trading session user
    /// </summary>
    public sealed class WeexSessionUser : TradingSessionUser
    {
        private readonly IWeexUserClientProvider _provider;
        private readonly WeexEnvironment? _environment;

        internal WeexSessionUser(
            TradingSessionUserDefinition definition,
            IWeexUserClientProvider provider,
            Func<IUserSpotDataTracker> getSpotTracker,
            Func<TradingMode, IUserFuturesDataTracker> getFuturesTracker)
            : base(definition, getSpotTracker, getFuturesTracker)
        {
            if (definition.Environment != null &&
                definition.Environment is not WeexEnvironment)
            {
                throw new ArgumentException(
                    "A WeexEnvironment is required.", nameof(definition));
            }

            _provider = provider;
            _environment = (WeexEnvironment?)definition.Environment;
        }

        //public PlaceSpotOrderOptions PlaceSpotOrderOptions =>
        //    _provider.GetPublicSharedApiClient(_environment)
        //        .SpotRest.PlaceSpotOrderOptions;

        //public async Task<HttpResult<SharedId>> PlaceSpotOrderAsync(
        //    PlaceSpotOrderRequest request,
        //    CancellationToken ct = default)
        //{
        //    var client = await _provider.TryGetSharedApiClientAsync(
        //        UserIdentifier, _environment, ct).ConfigureAwait(false);

        //    if (!client.Success)
        //    {
        //        return HttpResult.Fail<SharedId>(
        //            _provider.ExchangeName, client.Error);
        //    }

        //    return await client.Data.SpotRest
        //        .PlaceSpotOrderAsync(request, ct)
        //        .ConfigureAwait(false);
        //}
    }
}
