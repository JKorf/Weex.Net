using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Weex.Net.Clients.FuturesApi;
using Weex.Net.Clients.SpotApi;
using Weex.Net.Interfaces.Clients;
using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Interfaces.Clients.SpotApi;
using Weex.Net.Objects.Options;

namespace Weex.Net.Clients
{
    /// <inheritdoc cref="IWeexSocketClient" />
    public class WeexSocketClient : BaseSocketClient<WeexEnvironment, WeexCredentials>, IWeexSocketClient
    {
        #region fields
        #endregion

        #region Api clients
                
         /// <inheritdoc />
        public IWeexSocketClientFuturesApi FuturesApi { get; }

         /// <inheritdoc />
        public IWeexSocketClientSpotApi SpotApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of WeexSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public WeexSocketClient(Action<WeexSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        {
        }

        /// <summary>
        /// Create a new instance of WeexSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="options">Option configuration</param>
        public WeexSocketClient(IOptions<WeexSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Weex")
        {
            Initialize(options.Value);
                        
            FuturesApi = AddApiClient(new WeexSocketClientFuturesApi(this, _logger, options.Value));
            SpotApi = AddApiClient(new WeexSocketClientSpotApi(this, _logger, options.Value));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<WeexSocketOptions> optionsDelegate)
        {
            WeexSocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }
    }
}
