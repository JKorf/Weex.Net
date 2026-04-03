using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
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
    public class SharedRequestRequestTests
    {
        [Test]
        public async Task ValidateSpotAccountCalls()
        {
            var client = new WeexRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new WeexCredentials("KEY", "SEC", "PASS");
            });

            var symbol = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");
            var tester = new SharedRestRequestValidator<WeexRestClient>(client, "Endpoints/Spot/Account", "https://api-spot.weex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetBalancesAsync(new GetBalancesRequest()), "GetAccountInfo", client.SpotApi.SharedClient.GetBalancesOptions,
                x => x[0].Asset == "BTC",
                x => x[0].Available == 0.004m);
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetDepositsAsync(new GetDepositsRequest()), "GetFundingBills", client.SpotApi.SharedClient.GetDepositsOptions);
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetFeesAsync(new GetFeeRequest(symbol)), "GetAccountInfo", client.SpotApi.SharedClient.GetFeeOptions,
                x => x.TakerFee == 0.1m,
                x => x.TakerFee == 0.1m);
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetWithdrawalsAsync(new GetWithdrawalsRequest()), "GetFundingBills", client.SpotApi.SharedClient.GetWithdrawalsOptions);
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var client = new WeexRestClient(opts =>
            {
                opts.AutoTimestamp = false;
            });

            var symbol = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");
            var tester = new SharedRestRequestValidator<WeexRestClient>(client, "Endpoints/Spot/ExchangeData", "https://api-spot.weex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetAssetAsync(new GetAssetRequest("USDT")), "GetAssets", client.SpotApi.SharedClient.GetAssetOptions,
                x => x.Name == "USDT",
                x => x.Networks[0].Name == "BEP20(BSC)",
                x => x.Networks[0].DepositEnabled == true,
                x => x.Networks[0].WithdrawEnabled == true,
                x => x.Networks[0].ContractAddress == "0x55d39832789f99059ff7545246999027b3197955",
                x => x.Networks[0].MinConfirmations == 11,
                x => x.Networks[0].MinWithdrawQuantity == 0.00001m,
                x => x.Networks[0].WithdrawFee == 0.000002m
                );
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetAssetsAsync(new GetAssetsRequest()), "GetAssets", client.SpotApi.SharedClient.GetAssetOptions);
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetSpotSymbolsAsync(new GetSymbolsRequest()), "GetExchangeInfo", client.SpotApi.SharedClient.GetSpotSymbolsOptions,
                 x => x[0].Name == "BTCUSDT",
                 x => x[0].BaseAsset == "BTC",
                 x => x[0].QuoteAsset == "USDT",
                 x => x[0].MinTradeQuantity == 0.0001m,
                 x => x[0].PriceDecimals == 8,
                 x => x[0].QuantityDecimals == 8,
                 x => x[0].Trading == true,
                 x => x[0].TradingMode == TradingMode.Spot
                 );
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetBookTickerAsync(new GetBookTickerRequest(symbol)), "GetBookTicker", client.SpotApi.SharedClient.GetBookTickerOptions,
                x => x.BestAskPrice == 68920.60m,
                x => x.BestAskQuantity == 1.102m,
                x => x.BestBidPrice == 68919.90m,
                x => x.BestBidQuantity == 2.480m
                );
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetKlinesAsync(new GetKlinesRequest(symbol, SharedKlineInterval.OneDay)), "GetKlines", client.SpotApi.SharedClient.GetKlinesOptions,
                x => x[0].OpenPrice == 68940.10m,
                x => x[0].HighPrice == 68955.00m,
                x => x[0].LowPrice == 68938.50m,
                x => x[0].ClosePrice == 68952.40m,
                x => x[0].Volume == 12.345m
                );
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetOrderBookAsync(new GetOrderBookRequest(symbol)), "GetOrderBook", client.SpotApi.SharedClient.GetOrderBookOptions,
                x => x.Asks[0].Price == 68950.20m,
                x => x.Asks[0].Quantity == 1.104m,
                x => x.Bids[0].Price == 68950.10m,
                x => x.Bids[0].Quantity == 2.345m
                );
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetRecentTradesAsync(new GetRecentTradesRequest(symbol)), "GetRecentTrades", client.SpotApi.SharedClient.GetRecentTradesOptions,
                x => x[0].Price == 68950.00m,
                x => x[0].Quantity == 0.002m,
                x => x[0].Side == SharedOrderSide.Sell,
                x => x[0].Timestamp == new DateTime(2025, 11, 30, 12, 33, 20, 456, DateTimeKind.Utc)
                );
        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            var client = new WeexRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new WeexCredentials("KEY", "SEC", "PASS");
            });

            var symbol = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");
            var tester = new SharedRestRequestValidator<WeexRestClient>(client, "Endpoints/Spot/Trading", "https://api-spot.weex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.PlaceSpotOrderAsync(new PlaceSpotOrderRequest(symbol, SharedOrderSide.Buy, SharedOrderType.Market, SharedQuantity.Base(1))), "PlaceOrder", client.SpotApi.SharedClient.PlaceSpotOrderOptions,
                x => x.Id == "702345678901234567");
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetSpotOrderAsync(new GetOrderRequest(symbol, "1")), "GetOrder", client.SpotApi.SharedClient.GetSpotOrderOptions,
                x => x.ClientOrderId == "my-spot-order-001",
                x => x.OrderId == "702345678901234567",
                x => x.OrderPrice == 68900m,
                x => x.OrderQuantity.QuantityInBaseAsset == 0.01m,
                x => x.OrderType == SharedOrderType.Limit,
                x => x.QuantityFilled.QuantityInBaseAsset == 0.01m,
                x => x.Side == SharedOrderSide.Buy,
                x => x.Status == SharedOrderStatus.Filled,
                x => x.Symbol == "BTCUSDT",
                x => x.CreateTime == new DateTime(2025, 11, 30, 12, 33, 20, 456, DateTimeKind.Utc),
                x => x.UpdateTime == new DateTime(2025, 11, 30, 12, 33, 20, 456, DateTimeKind.Utc)
                );
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetOpenSpotOrdersAsync(new GetOpenOrdersRequest()), "GetOpenOrders", client.SpotApi.SharedClient.GetOpenSpotOrdersOptions);
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetClosedSpotOrdersAsync(new GetClosedOrdersRequest(symbol)), "GetOrderHistory", client.SpotApi.SharedClient.GetClosedSpotOrdersOptions);
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetSpotOrderTradesAsync(new GetOrderTradesRequest(symbol, "1")), "GetUserTrades", client.SpotApi.SharedClient.GetSpotOrderTradesOptions,
                x => x[0].Id == "801234567890123456",
                x => x[0].Fee == 0.138m,
                x => x[0].OrderId == "702345678901234567",
                x => x[0].Price == 68950.00m,
                x => x[0].Quantity == 0.01m,
                x => x[0].Side == SharedOrderSide.Buy,
                x => x[0].Symbol == "BTCUSDT",
                x => x[0].Timestamp == new DateTime(2025, 11, 30, 12, 33, 20, 456, DateTimeKind.Utc)
                );
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.GetSpotUserTradesAsync(new GetUserTradesRequest(symbol)), "GetUserTrades", client.SpotApi.SharedClient.GetSpotUserTradesOptions);
            await tester.ValidateAsync(client => client.SpotApi.SharedClient.CancelSpotOrderAsync(new CancelOrderRequest(symbol, "1")), "CancelOrder", client.SpotApi.SharedClient.CancelSpotOrderOptions);
        }

        [Test]
        public async Task ValidateFuturesAccountCalls()
        {
            var client = new WeexRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new WeexCredentials("KEY", "SEC", "PASS");
            });

            var symbol = new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT");
            var tester = new SharedRestRequestValidator<WeexRestClient>(client, "Endpoints/Futures/Account", "https://api-contract.weex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetBalancesAsync(new GetBalancesRequest()), "GetBalances", client.FuturesApi.SharedClient.GetBalancesOptions,
                x => x[0].Asset == "USDT",
                x => x[0].Available == 5413.06877369m);
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetFeesAsync(new GetFeeRequest(symbol)), "GetTradingFees", client.FuturesApi.SharedClient.GetFeeOptions,
                x => x.MakerFee == 0.02m,
                x => x.TakerFee == 0.04m);
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetLeverageAsync(new GetLeverageRequest(symbol)), "GetSymbolConfig", client.FuturesApi.SharedClient.GetLeverageOptions,
                x => x.Leverage == 20);
        }

        [Test]
        public async Task ValidateFuturesExchangeDataCalls()
        {
            var client = new WeexRestClient(opts =>
            {
                opts.AutoTimestamp = false;
            });

            var symbol = new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT");
            var tester = new SharedRestRequestValidator<WeexRestClient>(client, "Endpoints/Futures/ExchangeData", "https://api-contract.weex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetFuturesSymbolsAsync(new GetSymbolsRequest()), "GetExchangeInfo", client.FuturesApi.SharedClient.GetFuturesSymbolsOptions,
                x => x[0].Name == "BTCUSDT",
                x => x[0].BaseAsset == "BTC",
                x => x[0].QuoteAsset == "USDT",
                x => x[0].ContractSize == 1m,
                x => x[0].PriceDecimals == 1,
                x => x[0].QuantityDecimals == 6,
                x => x[0].Trading == true
                );
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetBookTickerAsync(new GetBookTickerRequest(symbol)), "GetBookTicker", client.FuturesApi.SharedClient.GetBookTickerOptions,
                x => x.BestAskPrice == 69351.0m,
                x => x.BestAskQuantity == 8.3m,
                x => x.BestBidPrice == 69350.1m,
                x => x.BestBidQuantity == 12.5m);
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetFundingRateHistoryAsync(new GetFundingRateHistoryRequest(symbol)), "GetFundingRateHistory", client.FuturesApi.SharedClient.GetFundingRateHistoryOptions,
                x => x[0].FundingRate == 0.00025m,
                x => x[0].Timestamp == new DateTime(2025, 11, 30, 12, 33, 20, 456, DateTimeKind.Utc)
                );
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetKlinesAsync(new GetKlinesRequest(symbol, SharedKlineInterval.OneHour)), "GetKlines", client.FuturesApi.SharedClient.GetKlinesOptions,
                x => x[0].OpenPrice == 69340.1m,
                x => x[0].HighPrice == 69380.0m,
                x => x[0].LowPrice == 69320.5m,
                x => x[0].ClosePrice == 69355.2m,
                x => x[0].Volume == 25.678m
                );
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetOpenInterestAsync(new GetOpenInterestRequest(symbol)), "GetOpenInterest", client.FuturesApi.SharedClient.GetOpenInterestOptions,
                x => x.OpenInterest == 128345.67m
                );
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetOrderBookAsync(new GetOrderBookRequest(symbol)), "GetOrderBook", client.FuturesApi.SharedClient.GetOrderBookOptions,
                x => x.Asks[0].Price == 69351.0m,
                x => x.Asks[0].Quantity == 3.6m,
                x => x.Bids[0].Price == 69350.1m,
                x => x.Bids[0].Quantity == 12.5m
                );
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetRecentTradesAsync(new GetRecentTradesRequest(symbol)), "GetTrades", client.FuturesApi.SharedClient.GetRecentTradesOptions,
                x => x[0].Price == 69350.8m,
                x => x[0].Quantity == 0.005m,
                x => x[0].Timestamp == new DateTime(2025, 11, 30, 12, 33, 20, 456, DateTimeKind.Utc),
                x => x[0].Side == SharedOrderSide.Buy
                );
        }

        [Test]
        public async Task ValidateFuturesTradingCalls()
        {
            var client = new WeexRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new WeexCredentials("KEY", "SEC", "PASS");
            });

            var symbol = new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT");
            var tester = new SharedRestRequestValidator<WeexRestClient>(client, "Endpoints/Futures/Trading", "https://api-contract.weex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.PlaceFuturesOrderAsync(new PlaceFuturesOrderRequest(symbol, SharedOrderSide.Buy, SharedOrderType.Market, SharedQuantity.Contracts(1), positionSide: SharedPositionSide.Long, marginMode: SharedMarginMode.Cross)), "PlaceOrder", client.FuturesApi.SharedClient.PlaceFuturesOrderOptions,
                x => x.Id == "702345678901234567");
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetFuturesOrderAsync(new GetOrderRequest(symbol, "1")), "GetOrder", client.FuturesApi.SharedClient.GetFuturesOrderOptions,
                x => x.ClientOrderId == "my-order-0001",
                x => x.OrderId == "702345678901234567",
                x => x.OrderPrice == 69000,
                x => x.AveragePrice == 68990.5m,
                x => x.OrderQuantity.QuantityInBaseAsset == 0.01m,
                x => x.OrderType == SharedOrderType.Limit,
                x => x.QuantityFilled.QuantityInBaseAsset == 0.01m,
                x => x.QuantityFilled.QuantityInQuoteAsset == 689.905m,
                x => x.Side == SharedOrderSide.Buy,
                x => x.Status == SharedOrderStatus.Filled,
                x => x.Symbol == "BTCUSDT",
                x => x.ReduceOnly == false,
                x => x.TimeInForce == SharedTimeInForce.GoodTillCanceled,
                x => x.PositionSide == SharedPositionSide.Long,
                x => x.CreateTime == new DateTime(2025, 11, 30, 12, 33, 20, 456, DateTimeKind.Utc),
                x => x.UpdateTime == new DateTime(2025, 11, 30, 12, 33, 20, 456, DateTimeKind.Utc)
                );
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.CancelFuturesOrderAsync(new CancelOrderRequest(symbol, "1")), "CancelOrder", client.FuturesApi.SharedClient.CancelFuturesOrderOptions);
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.CancelFuturesTriggerOrderAsync(new CancelOrderRequest(symbol, "1")), "CancelConditionalOrder", client.FuturesApi.SharedClient.CancelFuturesTriggerOrderOptions);
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetClosedFuturesOrdersAsync(new GetClosedOrdersRequest(symbol)), "GetOrderHistory", client.FuturesApi.SharedClient.GetClosedFuturesOrdersOptions);
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetFuturesOrderTradesAsync(new GetOrderTradesRequest(symbol, "1")), "GetUserTrades", client.FuturesApi.SharedClient.GetFuturesOrderTradesOptions,
                x => x[0].Id == "801234567890123456",
                x => x[0].OrderId == "702345678901234567",
                x => x[0].Symbol == "BTCUSDT",
                x => x[0].Side  == SharedOrderSide.Buy,
                x => x[0].Fee == 0.138m,
                x => x[0].FeeAsset == "USDT",
                x => x[0].Role == SharedRole.Taker,
                x => x[0].Price == 69000m,
                x => x[0].Quantity == 0.01m,
                x => x[0].Timestamp == new DateTime(2025, 11, 30, 12, 33, 20, 456, DateTimeKind.Utc)
                );
            await tester.ValidateAsync(client => client.FuturesApi.SharedClient.GetFuturesUserTradesAsync(new GetUserTradesRequest(symbol)), "GetUserTrades", client.FuturesApi.SharedClient.GetFuturesUserTradesOptions);
            
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.Contains("ACCESS-SIGN");
        }
    }
}
