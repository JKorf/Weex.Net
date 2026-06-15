using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Interfaces.Clients.SpotApi;
using Weex.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using Weex.Net.Clients.MessageHandlers;
using System.Collections.Generic;

namespace Weex.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IWeexRestClientSpotApi" />
    internal partial class WeexRestClientSpotApi : RestApiClient<WeexEnvironment, WeexAuthenticationProvider, WeexCredentials>, IWeexRestClientSpotApi
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
        public IWeexRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IWeexRestClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IWeexRestClientSpotApiTrading Trading { get; }
        #endregion

        #region constructor/destructor
        internal WeexRestClientSpotApi(WeexRestClient baseClient, ILoggerFactory? loggerFactory, HttpClient? httpClient, WeexRestOptions options)
            : base(loggerFactory, WeexExchange.Metadata.Id, httpClient, options.Environment.RestClientSpotAddress, options, options.SpotOptions)
        {
            Account = new WeexRestClientSpotApiAccount(this);
            ExchangeData = new WeexRestClientSpotApiExchangeData(_logger, this);
            Trading = new WeexRestClientSpotApiTrading(_logger, this);

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

        internal async Task<HttpResult> SendAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<Unit>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            return result;
        }

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>( definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            return result;
        }

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? uriParameters, Parameters? bodyParameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(definition, uriParameters, bodyParameters, cancellationToken, null, weight).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null) 
            => WeexExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        /// <inheritdoc />
        public IWeexRestClientSpotApiShared SharedClient => this;
    }
}
