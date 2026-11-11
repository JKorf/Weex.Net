using CryptoExchange.Net.Sessions;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Sessions.Features;
using CryptoExchange.Net.Trackers.Sessions.Interfaces;
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
        where TStrategy : TradingStrategy, IOrderBookUpdateHandler
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        var symbol = builder.Symbol;
        var environment = builder.Environment;
        if (symbol.TradingMode != TradingMode.Spot && symbol.TradingMode != TradingMode.PerpetualLinear)
            throw new NotSupportedException($"Weex order books do not support {symbol.TradingMode}.");

        var factory = builder.OrderBooks;
        return builder.RegisterOrderBook(strategy =>
            new OrderBookFeature(factory.Create(symbol, options, (WeexEnvironment?)environment), symbol, strategy));
    }
}
