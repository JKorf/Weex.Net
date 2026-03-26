using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Weex.Net.Clients;
using Weex.Net.Objects.Options;

namespace Weex.Net.UnitTests
{
    [NonParallelizable]
    public class WeexRestIntegrationTests : RestIntegrationTest<WeexRestClient>
    {
        public override bool Run { get; set; } = true;

        public override WeexRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");
            var pass = Environment.GetEnvironmentVariable("PASS");

            Authenticated = key != null && sec != null;
            return new WeexRestClient(null, loggerFactory, Options.Create(new WeexRestOptions
            {
                AutoTimestamp = false,
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new WeexCredentials(key, sec, pass) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().SpotApi.ExchangeData.GetRecentTradesAsync("AAAUSDT", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.Code, Is.EqualTo(-1142));
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            //await RunAndCheckResult(client => client.SpotApi.ExchangeData.PingAsync(CancellationToken.None), false, true);
        }
    }
}
