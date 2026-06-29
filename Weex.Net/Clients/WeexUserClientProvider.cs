using Weex.Net.Interfaces.Clients;
using Weex.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using CryptoExchange.Net.Clients;

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
        
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public WeexUserClientProvider(Action<WeexOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }
        
        /// <summary>
        /// ctor
        /// </summary>
        public WeexUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<WeexRestOptions> restOptions,
            IOptions<WeexSocketOptions> socketOptions)
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        public override string ExchangeName => WeexExchange.Metadata.Id;

        /// <inheritdoc />
        protected override IWeexRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<WeexRestOptions> options) 
            => new WeexRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override IWeexSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<WeexSocketOptions> options)
            => new WeexSocketClient(options, loggerFactory);
    }
}
