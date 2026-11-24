using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData;
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

            IGetRecentTradesRest? sharedRestClient;
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

            return CreateSpotTracker(client, null, config);
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, WeexCredentials credentials, SpotUserDataTrackerConfig? config = null, WeexEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IWeexUserClientProvider>() ?? new WeexUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            var client = new WeexSharedApiClient(restClient, socketClient, Options.Create(WeexOptions.Create()));
            return CreateSpotTracker(client, userIdentifier, config);
        }

        /// <inheritdoc />
        public async Task<CallResult<IUserSpotDataTracker>> TryCreateUserSpotDataTrackerAsync(
            string userIdentifier,
            SpotUserDataTrackerConfig? config = null,
            WeexEnvironment? environment = null,
            CancellationToken ct = default)
        {
            var provider = _serviceProvider?.GetRequiredService<IWeexUserClientProvider>()
                ?? new WeexUserClientProvider();

            var client = await provider.TryGetSharedApiClientAsync(userIdentifier, environment, ct).ConfigureAwait(false);
            if (!client.Success)
                return CallResult.Fail<IUserSpotDataTracker>(client.Error);

            return CallResult.Ok(CreateSpotTracker(client.Data, userIdentifier, config));
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(FuturesUserDataTrackerConfig? config = null)
        {
            var client = _serviceProvider?.GetRequiredService<IWeexSharedApiClient>() ?? new WeexSharedApiClient(
                new WeexRestClient(), new WeexSocketClient(), Options.Create(WeexOptions.Create()));

            return CreateFuturesTracker(client, null, TradingMode.PerpetualLinear, config);
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, WeexCredentials credentials, FuturesUserDataTrackerConfig? config = null, WeexEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IWeexUserClientProvider>() ?? new WeexUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            var client = new WeexSharedApiClient(restClient, socketClient, Options.Create(WeexOptions.Create()));
            return CreateFuturesTracker(client, userIdentifier, TradingMode.PerpetualLinear, config);
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
            ValidateFuturesMode(mode);

            var provider = _serviceProvider?.GetRequiredService<IWeexUserClientProvider>()
                ?? new WeexUserClientProvider();

            var client = await provider.TryGetSharedApiClientAsync(userIdentifier, environment, ct).ConfigureAwait(false);

            if (!client.Success)
                return CallResult.Fail<IUserFuturesDataTracker>(client.Error);

            return CallResult.Ok(CreateFuturesTracker(client.Data, userIdentifier, mode, config));
        }

        private IUserSpotDataTracker CreateSpotTracker(
            ISharedApiClientBase client,
            string? userIdentifier,
            SpotUserDataTrackerConfig? config)
        {
            var logger = _serviceProvider?
                .GetRequiredService<ILogger<UserSpotDataTracker>>()
                ?? NullLogger<UserSpotDataTracker>.Instance;

            return new UserSpotDataTracker(
                logger,
                client,
                userIdentifier,
                config ?? new SpotUserDataTrackerConfig());
        }

        private IUserFuturesDataTracker CreateFuturesTracker(
            ISharedApiClientBase client,
            string? userIdentifier,
            TradingMode mode,
            FuturesUserDataTrackerConfig? config)
        {
            ValidateFuturesMode(mode);

            var logger = _serviceProvider?
                .GetRequiredService<ILogger<UserFuturesDataTracker>>()
                ?? NullLogger<UserFuturesDataTracker>.Instance;

            return new UserFuturesDataTracker(
                logger,
                client,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig(),
                websocketPositionUpdatesAreFullSnapshots: false,
                accountType: SharedAccountType.PerpetualLinearFutures);
        }

        private static void ValidateFuturesMode(TradingMode mode)
        {
            if (mode != TradingMode.PerpetualLinear)
            {
                throw new NotSupportedException(
                    $"Weex futures tracking does not support {mode}.");
            }
        }
    }
}
