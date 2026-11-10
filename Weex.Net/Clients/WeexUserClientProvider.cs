using Weex.Net.Interfaces.Clients;
using Weex.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using CryptoExchange.Net.Clients;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Interfaces.Clients;
using System.Threading;

namespace Weex.Net.Clients
{
    /// <inheritdoc />
    public class WeexUserClientProvider : UserClientProvider<
        IWeexRestClient,
        IWeexSocketClient,
        WeexRestOptions,
        WeexSocketOptions,
        WeexCredentials,
        WeexEnvironment
        >, IWeexUserClientProvider
    {
        private readonly IOptions<WeexOptions> _options;
        private ConcurrentDictionary<string, WeexSharedApiClient> _sharedPublicClients = new();

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public WeexUserClientProvider(Action<WeexOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }
        
        /// <summary>
        /// ctor
        /// </summary>
        public WeexUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<WeexOptions> options)
            : base(httpClient, loggerFactory, Options.Create(options.Value.Rest), Options.Create(options.Value.Socket), options.Value.CredentialResolver)
        {
            _options = options;
        }

        /// <inheritdoc />
        public override string ExchangeName => WeexExchange.Metadata.Id;

        /// <inheritdoc />
        async Task<CallResult<ISharedApiClientBase>> ISharedUserClientProvider.TryGetSharedApiClientAsync(string userIdentifier, TradeEnvironment? environment, CancellationToken ct)
        {
            var result = await TryGetSharedApiClientAsync(userIdentifier, (WeexEnvironment?)environment, ct).ConfigureAwait(false);
            return result.Success ? CallResult.Ok<ISharedApiClientBase>(result.Data) : CallResult.Fail<ISharedApiClientBase>(result.Error);
        }
        ISharedApiClientBase ISharedUserClientProvider.GetPublicSharedApiClient(TradeEnvironment? environment) => GetPublicSharedApiClient((WeexEnvironment?)environment);

        public IWeexSharedApiClient GetPublicSharedApiClient(WeexEnvironment? environment) =>
            _sharedPublicClients.GetOrAdd(environment?.Name ?? "default", _ =>
                new WeexSharedApiClient(
                    GetPublicRestClient(environment), 
                    GetPublicSocketClient(environment),
                _options));

        public async Task<CallResult<IWeexSharedApiClient>> TryGetSharedApiClientAsync(string userIdentifier, WeexEnvironment? environment = null, CancellationToken ct = default)
        {
#warning cache clients in base provider
            var rest = await TryGetRestClientAsync(userIdentifier, environment, ct).ConfigureAwait(false);
            if (!rest.Success)
                return CallResult.Fail<IWeexSharedApiClient>(rest.Error);

            var socket = await TryGetSocketClientAsync(userIdentifier, environment, ct).ConfigureAwait(false);
            if (!socket.Success)
                return CallResult.Fail<IWeexSharedApiClient>(socket.Error);

            return CallResult.Ok<IWeexSharedApiClient>(new WeexSharedApiClient(rest.Data, socket.Data, _options));
        }

        /// <inheritdoc />
        protected override IWeexRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<WeexRestOptions> options) 
            => new WeexRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override IWeexSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<WeexSocketOptions> options)
            => new WeexSocketClient(options, loggerFactory);

    }
}
