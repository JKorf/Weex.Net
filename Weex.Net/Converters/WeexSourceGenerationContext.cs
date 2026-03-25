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

    [JsonSerializable(typeof(WeexSocketEvent<WeexKlineUpdate[]>))]
    [JsonSerializable(typeof(WeexBookTickerUpdate))]
    [JsonSerializable(typeof(WeexOrderBookUpdate))]
    [JsonSerializable(typeof(WeexSocketEvent<WeexTickerUpdate[]>))]
    [JsonSerializable(typeof(WeexSocketRequest))]
    [JsonSerializable(typeof(WeexSocketResponse))]
    [JsonSerializable(typeof(WeexPing))]
    [JsonSerializable(typeof(WeexPong))]

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
