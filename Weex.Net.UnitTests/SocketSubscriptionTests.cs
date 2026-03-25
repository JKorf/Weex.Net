using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Threading.Tasks;
using Weex.Net.Clients;
using Weex.Net.Objects.Models;
using Weex.Net.Objects.Options;

namespace Weex.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateSubscriptions()
        {
            var client = new WeexSocketClient(opts =>
            {
                opts.ApiCredentials = new WeexCredentials("123", "456", "789");
            });
            var tester = new SocketSubscriptionValidator<WeexSocketClient>(client, "Subscriptions/Spot", "XXX");
            //await tester.ValidateAsync<WeexModel>((client, handler) => client.SpotApi.SubscribeToXXXUpdatesAsync(handler), "XXX");
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task ValidateConcurrentSpotSubscriptions(bool newDeserialization)
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new WeexSocketClient(Options.Create(new WeexSocketOptions
            {
                ApiCredentials = new WeexCredentials("123", "456", "789"),
                OutputOriginalData = true
            }), logger);

            var tester = new SocketSubscriptionValidator<WeexSocketClient>(client, "Subscriptions/Spot", "XXX", "data");
            //await tester.ValidateConcurrentAsync<WeexModel>(
            //    (client, handler) => client.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync("BTCUSDT", Enums.KlineInterval.EightHour, handler),
            //    (client, handler) => client.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync("BTCUSDT", Enums.KlineInterval.OneHour, handler),
            //    "Concurrent");
        }
    }
}
