using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.TradeSessions.Features;
using CryptoExchange.Net.TradeSessions;
using System;
using Weex.Net.Objects.Options;

namespace Weex.Net;

/// <summary>
/// Trading session symbol extension methods
/// </summary>
public static class WeexTradingSessionSymbolExtensions
{
    public static WeexTradingSessionSymbolBuilder<TStrategy> AddOrderBookTracking<TStrategy>(
        this WeexTradingSessionSymbolBuilder<TStrategy> builder,
        Action<WeexOrderBookOptions>? options = null)
        where TStrategy : TradeSession
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        var symbol = builder.Symbol;
        var environment = builder.Environment;
        if (symbol.TradingMode != TradingMode.Spot && symbol.TradingMode != TradingMode.PerpetualLinear)
            throw new NotSupportedException($"Weex order books do not support {symbol.TradingMode}.");

        var factory = builder.OrderBooks;
        return builder.RegisterOrderBook(_ =>
            new OrderBookFeature(factory.Create(symbol, options, (WeexEnvironment?)environment), symbol));
    }
}
