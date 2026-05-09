using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using Weex.Net.Clients.MessageHandlers;
using System.Collections.Generic;

namespace Weex.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="IWeexRestClientFuturesApi" />
    internal partial class WeexRestClientFuturesApi : RestApiClient<WeexEnvironment, WeexAuthenticationProvider, WeexCredentials>, IWeexRestClientFuturesApi
    {
        #region fields 
        protected override ErrorMapping ErrorMapping => WeexErrors.RestErrors;

        /// <inheritdoc />
        public new WeexRestOptions ClientOptions => (WeexRestOptions)base.ClientOptions;

        /// <inheritdoc />
        protected override IRestMessageHandler MessageHandler { get; } = new WeexRestMessageHandler(WeexErrors.RestErrors);
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IWeexRestClientFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IWeexRestClientFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IWeexRestClientFuturesApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "Weex";
        #endregion

        #region constructor/destructor
        internal WeexRestClientFuturesApi(WeexRestClient baseClient, ILogger logger, HttpClient? httpClient, WeexRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientFuturesAddress, options, options.FuturesOptions)
        {
            Account = new WeexRestClientFuturesApiAccount(this);
            ExchangeData = new WeexRestClientFuturesApiExchangeData(logger, this);
            Trading = new WeexRestClientFuturesApiTrading(logger, this);

            StandardRequestHeaders = new Dictionary<string, string>
            {
                { "User-Agent", "CryptoExchange.Net/" + baseClient.CryptoExchangeLibVersion }
            };
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(WeexExchange._serializerContext);


        /// <inheritdoc />
        protected override WeexAuthenticationProvider CreateAuthenticationProvider(WeexCredentials credentials)
            => new WeexAuthenticationProvider(credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            return result;
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            return result;
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? uriParameters, ParameterCollection? bodyParameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, uriParameters, bodyParameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? uriParameters, ParameterCollection? bodyParameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, uriParameters, bodyParameters, cancellationToken, null, weight).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null) 
            => WeexExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        /// <inheritdoc />
        public IWeexRestClientFuturesApiShared SharedClient => this;
    }
}
