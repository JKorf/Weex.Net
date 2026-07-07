using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Clients;
using Weex.Net.Objects.Options;

namespace Weex.Net.UnitTests
{
    [NonParallelizable]
    public class WeexRestIntegrationTests : RestIntegrationTest<WeexRestClient>
    {
        public override bool Run { get; set; } = false;

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
        public async Task TestSpotAccount()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetTradingSymbolsAsync(CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetAccountInfoAsync(CancellationToken.None), true, ignoreProperties: ["accountType"]);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetAccountBillsAsync(default, default, default, default, default, CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetFundingBillsAsync(default, default, default, default, default, default, CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetTransferHistoryAsync(default, default, default, default, default, CancellationToken.None), true);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetAssetsAsync(CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(default, default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetPricesAsync(default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetTickersAsync(default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT", default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT", default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetBookTickersAsync(default, CancellationToken.None), false);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotTrading()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetOpenOrdersAsync(default, CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetOrderHistoryAsync("ETHUSDT", default, default, default, default, CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT", default, default, default, default, CancellationToken.None), true);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestFuturesAccount()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetBalancesAsync(CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetTradingFeesAsync("ETHUSDT", CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetAccountConfigAsync(CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetSymbolConfigAsync(default, CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetAccountBillsAsync(default, default, default, default, default, default, CancellationToken.None), true);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestFuturesExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetServerTimeAsync(CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetExchangeInfoAsync(default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetTickersAsync(default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetRecentTradesAsync("ETHUSDT", default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.FuturesKlineInterval.OneDay, default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT", default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetBookTickersAsync(default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetIndexPriceKlinesAsync("ETHUSDT", Enums.FuturesKlineInterval.OneDay, default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETHUSDT", Enums.FuturesKlineInterval.OneDay, default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetKlineHistoryAsync("ETHUSDT", Enums.KlineInterval.OneDay, default, default, default, default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetPriceAsync("ETHUSDT", default, CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetOpenInterestAsync("ETHUSDT", CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetFundingRateAsync("ETHUSDT", CancellationToken.None), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync("ETHUSDT", default, default, default, CancellationToken.None), false);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestFuturesTrading()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetPositionsAsync(CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetOpenOrdersAsync(default, default, default, default, default, default, CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetOrderHistoryAsync("ETHUSDT", default, default, default, default, CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetUserTradesAsync(default, default, default, default, default, CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetOpenConditionalOrdersAsync(default, default, default, default, default, CancellationToken.None), true);
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetConditionalOrderHistoryAsync(default, default, default, default, CancellationToken.None), true);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }
    }
}
