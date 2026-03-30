using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Weex.Net.Objects;
using Weex.Net.Objects.Internal;
using Weex.Net.Objects.Models;
using Weex.Net.Objects.Sockets;

namespace Weex.Net.Converters
{
    [JsonSerializable(typeof(WeexExchangeInfo))]
    [JsonSerializable(typeof(WeexServerTime))]
    [JsonSerializable(typeof(WeexAsset[]))]
    [JsonSerializable(typeof(WeexPrice[]))]
    [JsonSerializable(typeof(WeexTicker[]))]
    [JsonSerializable(typeof(WeexTrade[]))]
    [JsonSerializable(typeof(WeexKline[]))]
    [JsonSerializable(typeof(WeexOrderBook))]
    [JsonSerializable(typeof(WeexBookTicker[]))]
    [JsonSerializable(typeof(WeexAccountInfo))]
    [JsonSerializable(typeof(WeexBill[]))]
    [JsonSerializable(typeof(WeexPage<WeexFundingBill>))]
    [JsonSerializable(typeof(WeexTransfer[]))]
    [JsonSerializable(typeof(WeexOrderResult))]
    [JsonSerializable(typeof(WeexCancelResult))]
    [JsonSerializable(typeof(WeexCancelResult[]))]
    [JsonSerializable(typeof(WeexCancelResultWrapper))]
    [JsonSerializable(typeof(WeexOrder))]
    [JsonSerializable(typeof(WeexOrder[]))]
    [JsonSerializable(typeof(WeexUserTrade[]))]
    [JsonSerializable(typeof(WeexOrderRequest[]))]

    [JsonSerializable(typeof(WeexOrderUpdate))]
    [JsonSerializable(typeof(WeexUserTradeUpdate))]
    [JsonSerializable(typeof(WeexAccountUpdate))]
    [JsonSerializable(typeof(WeexSocketEvent<WeexTradeUpdate[]>))]
    [JsonSerializable(typeof(WeexSocketEvent<WeexKlineUpdate[]>))]
    [JsonSerializable(typeof(WeexBookTickerUpdate))]
    [JsonSerializable(typeof(WeexOrderBookUpdate))]
    [JsonSerializable(typeof(WeexSocketEvent<WeexTickerUpdate[]>))]
    [JsonSerializable(typeof(WeexSocketRequest))]
    [JsonSerializable(typeof(WeexSocketResponse))]
    [JsonSerializable(typeof(WeexPing))]
    [JsonSerializable(typeof(WeexPong))]

    [JsonSerializable(typeof(WeexFuturesExchangeInfo))]
    [JsonSerializable(typeof(WeexFuturesTicker[]))]
    [JsonSerializable(typeof(WeexFuturesBookTicker[]))]
    [JsonSerializable(typeof(WeexFuturesPrice))]
    [JsonSerializable(typeof(WeexOpenInterest))]
    [JsonSerializable(typeof(WeexFundingInfo[]))]
    [JsonSerializable(typeof(WeexFundingHistory[]))]
    [JsonSerializable(typeof(WeexFuturesBalance[]))]
    [JsonSerializable(typeof(WeexTradingFee))]
    [JsonSerializable(typeof(WeexAccountConfig))]
    [JsonSerializable(typeof(WeexSymbolConfig[]))]
    [JsonSerializable(typeof(WeexFuturesBillPage))]
    [JsonSerializable(typeof(WeexPosition[]))]
    [JsonSerializable(typeof(WeexLeverageResult))]
    [JsonSerializable(typeof(WeexFuturesOrderResult))]
    [JsonSerializable(typeof(WeexFuturesOrderResult[]))]
    [JsonSerializable(typeof(WeexFuturesUserTrade[]))]
    [JsonSerializable(typeof(WeexFuturesConditionalOrder[]))]
    [JsonSerializable(typeof(WeexConditionalOrderPage))]
    [JsonSerializable(typeof(WeexResult))]
    [JsonSerializable(typeof(WeexPositionResult[]))]
    [JsonSerializable(typeof(WeexFuturesOrder))]
    [JsonSerializable(typeof(WeexFuturesOrder[]))]

    [JsonSerializable(typeof(WeexSocketEvent<WeexFuturesTickerUpdate[]>))]
    [JsonSerializable(typeof(WeexFuturesAccountUpdate))]
    [JsonSerializable(typeof(WeexPositionUpdate))]
    [JsonSerializable(typeof(WeexFuturesOrderUpdate))]
    [JsonSerializable(typeof(WeexFuturesUserTradeUpdate))]

    [JsonSerializable(typeof(IDictionary<string, object>))]
    [JsonSerializable(typeof(long[]))]
    [JsonSerializable(typeof(string[]))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(int?))]
    [JsonSerializable(typeof(int))]
    [JsonSerializable(typeof(long?))]
    [JsonSerializable(typeof(long))]
    [JsonSerializable(typeof(decimal))]
    [JsonSerializable(typeof(decimal?))]
    [JsonSerializable(typeof(DateTime))]
    [JsonSerializable(typeof(DateTime?))]
    internal partial class WeexSourceGenerationContext : JsonSerializerContext
    {
    }
}
