using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Clients;
using Weex.Net.Interfaces;
using Weex.Net.Interfaces.Clients;
using Weex.Net.Objects.Options;

namespace Weex.Net
{
    /// <inheritdoc />
    public class WeexTrackerFactory : IWeexTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public WeexTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public WeexTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval)
        {
            var client = _serviceProvider?.GetRequiredService<IWeexSocketClient>() ?? new WeexSocketClient();
            var options = symbol.TradingMode == TradingMode.Spot ? client.SpotApi.SharedClient.SubscribeKlineOptions : client.FuturesApi.SharedClient.SubscribeKlineOptions;
            return options.IsSupported(interval); 
        }

        /// <inheritdoc />
        public bool CanCreateTradeTracker(SharedSymbol symbol) => true;

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null, ExchangeParameters? exchangeParameters = null)
        {
            var client = _serviceProvider?.GetRequiredService<IWeexSharedApiClient>() ?? new WeexSharedApiClient(
                new WeexRestClient(), new WeexSocketClient(), Options.Create(WeexOptions.Create()));

            IGetKlinesRest sharedRestClient;
            ISubscribeKlinesSocket sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = client.SpotRest;
                sharedSocketClient = client.SpotSocket;
            }
            else
            {
                sharedRestClient = client.FuturesRest;
                sharedSocketClient = client.FuturesSocket;
            }

            return new KlineTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(client.Exchange),
                sharedRestClient,
                sharedSocketClient,
                symbol,
                interval,
                limit,
                period,
                exchangeParameters
                );
        }
        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null, ExchangeParameters? exchangeParameters = null)
        {
            var client = _serviceProvider?.GetRequiredService<IWeexSharedApiClient>() ?? new WeexSharedApiClient(
                new WeexRestClient(), new WeexSocketClient(), Options.Create(WeexOptions.Create()));

            IGetRecentTradesRest sharedRestClient;
            ISubscribeTradesSocket sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = client.SpotRest;
                sharedSocketClient = client.SpotSocket;
            }
            else
            {
                sharedRestClient = client.FuturesRest;
                sharedSocketClient = client.FuturesSocket;
            }

            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(client.Exchange),
                sharedRestClient,
                null,
                sharedSocketClient,
                symbol,
                limit,
                period,
                TradeQuantityType.BaseAsset,
                exchangeParameters
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null)
        {
            var client = _serviceProvider?.GetRequiredService<IWeexSharedApiClient>() ?? new WeexSharedApiClient(
                new WeexRestClient(), new WeexSocketClient(), Options.Create(WeexOptions.Create()));

            return new WeexUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<WeexUserSpotDataTracker>>() ?? new NullLogger<WeexUserSpotDataTracker>(),
                client,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, WeexCredentials credentials, SpotUserDataTrackerConfig? config = null, WeexEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IWeexUserClientProvider>() ?? new WeexUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);

            return new WeexUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<WeexUserSpotDataTracker>>() ?? new NullLogger<WeexUserSpotDataTracker>(),
                new WeexSharedApiClient(restClient, socketClient, Options.Create(WeexOptions.Create())),
                userIdentifier,
                config
                );
        }

        /// <inheritdoc />
        public async Task<CallResult<IUserSpotDataTracker>> TryCreateUserSpotDataTrackerAsync(
            string userIdentifier, 
            SpotUserDataTrackerConfig? config = null,
            WeexEnvironment? environment = null,
            CancellationToken ct = default)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IWeexUserClientProvider>() ?? new WeexUserClientProvider();
            var client = await clientProvider.TryGetSharedApiClientAsync(userIdentifier, environment, ct).ConfigureAwait(false);
            if (!client.Success)
                return CallResult.Fail<IUserSpotDataTracker>(client.Error!);

            return CallResult.Ok<IUserSpotDataTracker>(new WeexUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<WeexUserSpotDataTracker>>() ?? new NullLogger<WeexUserSpotDataTracker>(),
                client.Data,
                userIdentifier,
                config
                ));
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(FuturesUserDataTrackerConfig? config = null)
        {
            var client = _serviceProvider?.GetRequiredService<IWeexSharedApiClient>() ?? new WeexSharedApiClient(
                new WeexRestClient(), new WeexSocketClient(), Options.Create(WeexOptions.Create()));

            return new WeexUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<WeexUserFuturesDataTracker>>() ?? new NullLogger<WeexUserFuturesDataTracker>(),
                client,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, WeexCredentials credentials, FuturesUserDataTrackerConfig? config = null, WeexEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IWeexUserClientProvider>() ?? new WeexUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);

            return new WeexUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<WeexUserFuturesDataTracker>>() ?? new NullLogger<WeexUserFuturesDataTracker>(),
                new WeexSharedApiClient(restClient, socketClient, Options.Create(WeexOptions.Create())),
                userIdentifier,
                config
                );
        }

        public Task<CallResult<IUserFuturesDataTracker>> TryCreateUserFuturesDataTrackerAsync(
            string userIdentifier,
            FuturesUserDataTrackerConfig? config = null,
            WeexEnvironment? environment = null,
            CancellationToken ct = default)
            => TryCreateUserFuturesDataTrackerAsync(
                userIdentifier,
                TradingMode.PerpetualLinear,
                config,
                environment,
                ct);

        /// <inheritdoc />
        public async Task<CallResult<IUserFuturesDataTracker>> TryCreateUserFuturesDataTrackerAsync(
            string userIdentifier,
            TradingMode mode,
            FuturesUserDataTrackerConfig? config = null,
            WeexEnvironment? environment = null,
            CancellationToken ct = default)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IWeexUserClientProvider>() ?? new WeexUserClientProvider();
            var client = await clientProvider.TryGetSharedApiClientAsync(userIdentifier, environment, ct).ConfigureAwait(false);
            if (!client.Success)
                return CallResult.Fail<IUserFuturesDataTracker>(client.Error!);

            return CallResult.Ok<IUserFuturesDataTracker>(new WeexUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<WeexUserFuturesDataTracker>>() ?? new NullLogger<WeexUserFuturesDataTracker>(),
                client.Data,
                userIdentifier,
                config
                ));
        }
    }
}
