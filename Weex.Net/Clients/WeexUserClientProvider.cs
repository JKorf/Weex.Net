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
    public class WeexUserClientProvider : SharedUserClientProvider<
        IWeexRestClient,
        IWeexSocketClient,
        IWeexSharedApiClient,
        WeexRestOptions,
        WeexSocketOptions,
        WeexCredentials,
        WeexEnvironment>,
        IWeexUserClientProvider
    {
        private readonly IOptions<WeexOptions> _options;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public WeexUserClientProvider(Action<WeexOptions>? optionsDelegate = null)
            : this(
                null,
                null,
                Options.Create(WeexOptions.Create(optionsDelegate)))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public WeexUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<WeexOptions> options)
            : base(httpClient, loggerFactory, Options.Create(options.Value.Rest), Options.Create(options.Value.Socket), options.Value.UserClientProviderCredentialResolver)
        {
            _options = options;
        }

        /// <inheritdoc />
        public override string ExchangeName => WeexExchange.Metadata.Id;

        protected override IWeexRestClient ConstructRestClient(
            HttpClient client,
            ILoggerFactory? loggerFactory,
            IOptions<WeexRestOptions> options)
            => new WeexRestClient(client, loggerFactory, options);

        protected override IWeexSocketClient ConstructSocketClient(
            ILoggerFactory? loggerFactory,
            IOptions<WeexSocketOptions> options)
            => new WeexSocketClient(options, loggerFactory);

        protected override IWeexSharedApiClient ConstructSharedApiClient(
            IWeexRestClient restClient,
            IWeexSocketClient socketClient)
            => new WeexSharedApiClient(restClient, socketClient, _options);

    }
}
