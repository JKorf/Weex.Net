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
        public async Task ValidateSpotSubscriptions()
        {
            var client = new WeexSocketClient(opts =>
            {
                opts.ApiCredentials = new WeexCredentials("123", "456", "789");
            });
            var tester = new SocketSubscriptionValidator<WeexSocketClient>(client, "Subscriptions/Spot", "wss://ws-spot.weex.com");
            await tester.ValidateAsync<WeexBookTickerUpdate>((client, handler) => client.SpotApi.SubscribeToBookTickerUpdatesAsync("ETHUSDT", handler), "BookTicker");
            await tester.ValidateAsync<WeexTickerUpdate>((client, handler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", handler), "Ticker", "d.0");
            await tester.ValidateAsync<WeexOrderBookUpdate>((client, handler) => client.SpotApi.SubscribeToOrderBookUpdatesAsync("ETHUSDT", 15, handler), "OrderBook");
            await tester.ValidateAsync<WeexKlineUpdate[]>((client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneDay, handler), "Kline", "d");
            await tester.ValidateAsync<WeexTradeUpdate[]>((client, handler) => client.SpotApi.SubscribeToTradeUpdatesAsync("ETHUSDT", handler), "Trades", "d");

            await tester.ValidateAsync<WeexAccountUpdate>((client, handler) => client.SpotApi.SubscribeToAccountUpdatesAsync(handler), "Account");
            await tester.ValidateAsync<WeexOrderUpdate>((client, handler) => client.SpotApi.SubscribeToOrderUpdatesAsync(handler), "Order");
            await tester.ValidateAsync<WeexUserTradeUpdate>((client, handler) => client.SpotApi.SubscribeToUserTradeUpdatesAsync(handler), "UserTrade");
        }

        [Test]
        public async Task ValidateFuturesSubscriptions()
        {
            var client = new WeexSocketClient(opts =>
            {
                opts.ApiCredentials = new WeexCredentials("123", "456", "789");
            });
            var tester = new SocketSubscriptionValidator<WeexSocketClient>(client, "Subscriptions/Futures", "wss://ws-contract.weex.com");
            await tester.ValidateAsync<WeexFuturesTickerUpdate>((client, handler) => client.FuturesApi.SubscribeToTickerUpdatesAsync("ETHUSDT", handler), "Ticker", "d.0");
            await tester.ValidateAsync<WeexKlineUpdate[]>((client, handler) => client.FuturesApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneDay, handler), "Kline", "d");
            await tester.ValidateAsync<WeexOrderBookUpdate>((client, handler) => client.FuturesApi.SubscribeToOrderBookUpdatesAsync("ETHUSDT", 15, handler), "OrderBook");
            await tester.ValidateAsync<WeexTradeUpdate[]>((client, handler) => client.FuturesApi.SubscribeToTradeUpdatesAsync("ETHUSDT", handler), "Trades", "d");

            await tester.ValidateAsync<WeexFuturesAccountUpdate>((client, handler) => client.FuturesApi.SubscribeToAccountUpdatesAsync(handler), "Account", "d");
            await tester.ValidateAsync<WeexPositionUpdate>((client, handler) => client.FuturesApi.SubscribeToPositionUpdatesAsync(handler), "Position", "d");
            await tester.ValidateAsync<WeexFuturesOrderUpdate>((client, handler) => client.FuturesApi.SubscribeToOrderUpdatesAsync(handler), "Order", "d");
            await tester.ValidateAsync<WeexFuturesUserTradeUpdate>((client, handler) => client.FuturesApi.SubscribeToUserTradeUpdatesAsync(handler), "UserTrade", "d");
        }

        [TestCase]
        public async Task ValidateConcurrentSpotSubscriptions()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new WeexSocketClient(Options.Create(new WeexSocketOptions
            {
                OutputOriginalData = true
            }), logger);

            var tester = new SocketSubscriptionValidator<WeexSocketClient>(client, "Subscriptions/Spot", "wss://ws-spot.weex.com");
            await tester.ValidateConcurrentAsync<WeexKlineUpdate[]>(
                (client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneDay, handler),
                (client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneHour, handler),
                "Concurrent");
        }
    }
}
