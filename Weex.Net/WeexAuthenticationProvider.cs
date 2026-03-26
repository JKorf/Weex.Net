using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using System.Collections.Generic;

namespace Weex.Net
{
    internal class WeexAuthenticationProvider : AuthenticationProvider<WeexCredentials, WeexCredentials>
    {
        private static readonly IStringMessageSerializer _messageSerializer =
            new SystemTextJsonMessageSerializer(WeexExchange._serializerContext);

        public WeexAuthenticationProvider(WeexCredentials credentials) : base(credentials, credentials)
        {
        }


        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration requestConfig)
        {
            if (!requestConfig.Authenticated)
                return;

            var time = GetMillisecondTimestampLong(apiClient);
            var signString = time + requestConfig.Method.ToString().ToUpper() + requestConfig.Path;
            if (requestConfig.QueryParameters?.Count > 0)
            {
                var queryString = "?" + requestConfig.GetQueryString();
                signString += queryString;
                requestConfig.SetQueryString(queryString);
            }

            if (requestConfig.BodyParameters != null)
            {
                var body = GetSerializedBody(_messageSerializer, requestConfig.BodyParameters);
                signString += body;
                requestConfig.SetBodyContent(body);
            }

            var signature = SignHMACSHA256(signString, SignOutputType.Base64);

            requestConfig.Headers ??= new Dictionary<string, string>();
            requestConfig.Headers.Add("ACCESS-Key", Key);
            requestConfig.Headers.Add("ACCESS-SIGN", signature);
            requestConfig.Headers.Add("ACCESS-TIMESTAMP", time.ToString());
            requestConfig.Headers.Add("ACCESS-PASSPHRASE", Credential.Pass);
        }

        public void ApplyWebsocketAuthentication(SocketApiClient apiClient, IDictionary<string, string> headers)
        {
            var timestamp = GetMillisecondTimestampLong(apiClient);
            headers["ACCESS-KEY"] = Key;
            headers["ACCESS-PASSPHRASE"] = Credential.Pass;
            headers["ACCESS-TIMESTAMP"] = timestamp.ToString();
            headers["ACCESS-SIGN"] = SignHMACSHA256(timestamp + "/v3/ws/private", SignOutputType.Base64);
        }
    }
}
