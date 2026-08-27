using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using Weex.Net.Clients;

namespace Weex.Net.UnitTests
{
    [TestFixture()]
    public class WeexRestClientTests
    {
        [Test]
        public void CheckSignatureExample1()
        {
            var authProvider = new WeexAuthenticationProvider(new WeexCredentials("XXX", "XXX", "XXX"));
            var client = (RestApiClient)new WeexRestClient().SpotApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v3/order",
                (uriParams, bodyParams, headers) =>
                {
                    return headers["ACCESS-SIGN"].ToString();
                },
                "AkV6bNpe7++1vpD331UuwdWXsGC8Qy2HheZ29ilIwOQ=",
                new Parameters(WeexExchange._parameterSerializationSettings)
                {
                    { "symbol", "LTCBTC" },
                },
                DateTimeConverter.ParseFromDouble(1499827319559),
                false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<WeexRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<WeexSocketClient>();
        }

        [Test]
        public void TestFuturesRestSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = TestHelpers.ValidateSharedApi(new WeexRestClient().FuturesApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestFuturesSocketSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = TestHelpers.ValidateSharedApi(new WeexSocketClient().FuturesApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestSpotRestSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = TestHelpers.ValidateSharedApi(new WeexRestClient().SpotApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestSpotSocketSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = TestHelpers.ValidateSharedApi(new WeexSocketClient().SpotApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }
    }
}
