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

        [Test]
        public async Task ValidateFuturesAccountCalls()
        {
            var client = new WeexRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new WeexCredentials("123", "456", "789");
            });
            var tester = new RestRequestValidator<WeexRestClient>(client, "Endpoints/Futures/Account", "https://api-contract.weex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetBalancesAsync(), "GetBalances");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetTradingFeesAsync("123"), "GetTradingFees");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetAccountConfigAsync(), "GetAccountConfig");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetSymbolConfigAsync(), "GetSymbolConfig");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetAccountBillsAsync(), "GetAccountBills");
            await tester.ValidateAsync(client => client.FuturesApi.Account.SetMarginModeAsync("123", MarginType.Isolated), "SetMarginMode");
            await tester.ValidateAsync(client => client.FuturesApi.Account.SetLeverageAsync("123"), "SetLeverage");
            await tester.ValidateAsync(client => client.FuturesApi.Account.AdjustIsolatedMarginAsync(123, 0.1m, MarginAdjustType.Increase), "AdjustIsolatedMargin");
            await tester.ValidateAsync(client => client.FuturesApi.Account.SetAutoAppendMarginAsync(123, true), "SetAutoAppendMargin");
        }

        [Test]
        public async Task ValidateFuturesExchangeDataCalls()
        {
            var client = new WeexRestClient(opts =>
            {
                opts.AutoTimestamp = false;
            });
            var tester = new RestRequestValidator<WeexRestClient>(client, "Endpoints/Futures/ExchangeData", "https://api-contract.weex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetOrderBookAsync("123"), "GetOrderBook");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetTickersAsync("123"), "GetTickers");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetBookTickersAsync(), "GetBookTicker");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetRecentTradesAsync("123"), "GetTrades");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetKlinesAsync("123", FuturesKlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetIndexPriceKlinesAsync("123", FuturesKlineInterval.OneDay), "GetIndexPriceKlines");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetMarkPriceKlinesAsync("123", FuturesKlineInterval.OneDay), "GetMarkPriceKlines");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetKlineHistoryAsync("123", KlineInterval.OneDay), "GetKlineHistory");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetPriceAsync("123"), "GetPrice");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetOpenInterestAsync("123"), "GetOpenInterest");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetFundingRateAsync(), "GetFundingRate");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync("123"), "GetFundingRateHistory");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetTradingSymbolsAsync(), "GetTradingSymbols");
        }

        [Test]
        public async Task ValidateFuturesTradingCalls()
        {
            var client = new WeexRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new WeexCredentials("123", "456", "789");
            });
            var tester = new RestRequestValidator<WeexRestClient>(client, "Endpoints/Futures/Trading", "https://api-contract.weex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetPositionsAsync(), "GetPositions");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetPositionAsync("123"), "GetPosition");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.PlaceOrderAsync("123", OrderSide.Buy, PositionSide.Long, OrderType.Market, 0.1m), "PlaceOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.CancelOrderAsync(), "CancelOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.ClosePositionsAsync(), "ClosePositions");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrderAsync(123), "GetOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOpenOrdersAsync(), "GetOpenOrders");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrderHistoryAsync(), "GetOrderHistory");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetUserTradesAsync(), "GetUserTrades");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.PlaceConditionalOrderAsync("123", OrderSide.Buy, PositionSide.Long, FuturesOrderType.Stop, 0.1m, 0.1m), "PlaceConditionalOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.CancelConditionalOrderAsync(123), "CancelConditionalOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.CancelAllConditionalOrdersAsync(), "CancelAllConditionalOrders");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOpenConditionalOrdersAsync(), "GetOpenConditionalOrders");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetConditionalOrderHistoryAsync(), "GetConditionalOrderHistory");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.PlaceTpSlOrderAsync("123", PlanType.TakeProfit, 0.1m, 0.1m, PositionSide.Long), "PlaceTpSlOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.EditTpSlOrderAsync(123, 0.1m), "EditTpSlOrder");

        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.Contains("ACCESS-SIGN");
        }
    }
}
