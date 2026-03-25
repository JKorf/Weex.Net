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

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSubscriptions(bool newDeserialization)
        {
            //await RunAndCheckUpdate<>(newDeserialization , (client, updateHandler) => client.SpotApi.Account.SubscribeToUserDataUpdatesAsync(default, default, default, default, default, default, default, default), false, true);
            //await RunAndCheckUpdate<>(newDeserialization, (client, updateHandler) => client.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync("ETHUSDT", updateHandler, default), true, false);
        } 
    }
}
