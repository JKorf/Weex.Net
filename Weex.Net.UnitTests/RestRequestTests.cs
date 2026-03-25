using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weex.Net.Clients;
using Weex.Net.Enums;

namespace Weex.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateSpotAccountCalls()
        {
            var client = new WeexRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new WeexCredentials("123", "456", "789");
            });
            var tester = new RestRequestValidator<WeexRestClient>(client, "Endpoints/Spot/Account", "https://api-spot.weex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.Account.GetTradingSymbolsAsync(), "GetTradingSymbols");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountInfoAsync(), "GetAccountInfo");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountBillsAsync(), "GetAccountBills");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetFundingBillsAsync(), "GetFundingBills");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetTransferHistoryAsync(), "GetTransferHistory");
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var client = new WeexRestClient(opts =>
            {
                opts.AutoTimestamp = false;
            });
            var tester = new RestRequestValidator<WeexRestClient>(client, "Endpoints/Spot/ExchangeData", "https://api-spot.weex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetsAsync(), "GetAssets");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetPricesAsync(), "GetPrices");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTickersAsync(), "GetTickers");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT"), "GetRecentTrades");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlinesAsync("123", KlineInterval.OneMinute), "GetKlines");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetOrderBookAsync("123"), "GetOrderBook");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetBookTickersAsync(), "GetBookTicker");
        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            var client = new WeexRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new WeexCredentials("123", "456", "789");
            });
            var tester = new RestRequestValidator<WeexRestClient>(client, "Endpoints/Spot/Trading", "https://api-spot.weex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOrderAsync("123", OrderSide.Buy, OrderType.Limit, 0.1m), "PlaceOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrderAsync(123), "CancelOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelAllSymbolOrdersAsync("123"), "CancelAllSymbolOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrdersAsync(), "CancelOrders", nestedJsonProperty: "orderList");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderAsync(), "GetOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOpenOrdersAsync(), "GetOpenOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderHistoryAsync("123"), "GetOrderHistory");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetUserTradesAsync("123"), "GetUserTrades");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.Contains("ACCESS-SIGN");
        }
    }
}
