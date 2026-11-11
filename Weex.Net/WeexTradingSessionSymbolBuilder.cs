using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.TradeSessions;
using CryptoExchange.Net.TradeSessions.Interfaces;
using System;
using Weex.Net.Interfaces;

namespace Weex.Net;

/// <summary>
/// Symbol configuration builder
/// </summary>
public sealed class WeexTradingSessionSymbolBuilder<TStrategy>
    : TradingSessionSymbolBuilder<TStrategy, WeexTradingSessionSymbolBuilder<TStrategy>>,
        ITradeTrackingBuilder,
        ITickerTrackingBuilder,
        IKlineTrackingBuilder
    where TStrategy : TradeSession
{
    internal IWeexOrderBookFactory OrderBooks { get; }

    internal WeexTradingSessionSymbolBuilder(
        SharedSymbol symbol,
        IWeexOrderBookFactory orderBooks,
        WeexEnvironment? environment = null)
        : base(symbol, environment)
    {
        OrderBooks = orderBooks;
    }

    internal WeexTradingSessionSymbolBuilder<TStrategy> RegisterOrderBook(
        Func<TStrategy, ITradingSessionFeature> create)
    {
        return RegisterFeature("orderBook", create);
    }
}
