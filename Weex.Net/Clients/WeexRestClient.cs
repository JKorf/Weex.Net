using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using CryptoExchange.Net.Authentication;
using Weex.Net.Interfaces.Clients;
using Weex.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Options;
using CryptoExchange.Net.Objects.Options;
using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Interfaces.Clients.SpotApi;
using Weex.Net.Clients.FuturesApi;
using Weex.Net.Clients.SpotApi;

namespace Weex.Net.Clients
{
    /// <inheritdoc cref="IWeexRestClient" />
    public class WeexRestClient : BaseRestClient<WeexEnvironment, WeexCredentials>, IWeexRestClient
    {
        #region Api clients
                
         /// <inheritdoc />
        public IWeexRestClientFuturesApi FuturesApi { get; }

         /// <inheritdoc />
        public IWeexRestClientSpotApi SpotApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of the WeexRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public WeexRestClient(Action<WeexRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of the WeexRestClient using provided options
        /// </summary>
        /// <param name="options">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public WeexRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<WeexRestOptions> options) : base(loggerFactory, "Weex")
        {
            Initialize(options.Value);
            
            FuturesApi = AddApiClient(new WeexRestClientFuturesApi(_logger, httpClient, options.Value));
            SpotApi = AddApiClient(new WeexRestClientSpotApi(this, _logger, httpClient, options.Value));
        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<WeexRestOptions> optionsDelegate)
        {
            WeexRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }
    }
}
