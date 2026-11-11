using CryptoExchange.Net.Sessions;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Sessions.Interfaces;
using System;
using Weex.Net.Interfaces;

namespace Weex.Net
{
    /// <summary>
    /// Symbol configuration builder
    /// </summary>
    public sealed class WeexTradingSessionSymbolBuilder<TSession>
        : TradingSessionSymbolBuilder<TSession, WeexTradingSessionSymbolBuilder<TSession>>
        where TSession : TradingSession
    {
        internal IWeexOrderBookFactory OrderBooks { get; }

        internal WeexTradingSessionSymbolBuilder(SharedSymbol symbol, IWeexOrderBookFactory orderBooks, WeexEnvironment? environment = null)
            : base(symbol, environment)
        {
            OrderBooks = orderBooks;
        }

        internal WeexTradingSessionSymbolBuilder<TSession> RegisterOrderBook(
            Func<TSession, ITradingSessionFeature> create)
        {
            return RegisterFeature("orderBook", create);
        }
    }
}
