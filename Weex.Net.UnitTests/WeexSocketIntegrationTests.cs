using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Weex.Net.Clients;
using Weex.Net.Objects.Models;
using Weex.Net.Objects.Options;

namespace Weex.Net.UnitTests
{
    [NonParallelizable]
    internal class WeexSocketIntegrationTests : SocketIntegrationTest<WeexSocketClient>
    {
        public override bool Run { get; set; } = false;

        public WeexSocketIntegrationTests()
        {
        }

        public override WeexSocketClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");
            var pass = Environment.GetEnvironmentVariable("PASS");

            Authenticated = key != null && sec != null;
            return new WeexSocketClient(Options.Create(new WeexSocketOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new WeexCredentials(key, sec, pass) : null
            }), loggerFactory);
        }

        [Test]
        public async Task TestSubscriptions()
        {
            await RunAndCheckUpdate<WeexAccountUpdate>((client, updateHandler) => client.SpotApi.SubscribeToAccountUpdatesAsync(updateHandler, default), false, true);
            await RunAndCheckUpdate<WeexTickerUpdate>((client, updateHandler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", updateHandler, default), true, false);

            await RunAndCheckUpdate<WeexFuturesAccountUpdate>((client, updateHandler) => client.FuturesApi.SubscribeToAccountUpdatesAsync(updateHandler, default), false, true);
            await RunAndCheckUpdate<WeexFuturesTickerUpdate>((client, updateHandler) => client.FuturesApi.SubscribeToTickerUpdatesAsync("ETHUSDT", updateHandler, default), true, false);
        } 
    }
}
