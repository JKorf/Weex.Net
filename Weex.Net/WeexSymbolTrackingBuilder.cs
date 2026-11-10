using CryptoExchange.Net.Sessions;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Sessions.Interfaces;
using System;
using Weex.Net.Interfaces;

namespace Weex.Net
{
    public sealed class WeexSymbolTrackingBuilder<TSession>
        : SymbolTrackingBuilder<TSession, WeexSymbolTrackingBuilder<TSession>>
        where TSession : Session
    {
        internal IWeexOrderBookFactory OrderBooks { get; }

        internal WeexSymbolTrackingBuilder(SharedSymbol symbol, IWeexOrderBookFactory orderBooks, WeexEnvironment? environment = null)
            : base(symbol, environment)
        {
            OrderBooks = orderBooks;
        }

        internal WeexSymbolTrackingBuilder<TSession> RegisterOrderBook(
            Func<TSession, ISessionFeature> create)
        {
            return RegisterFeature("orderBook", create);
        }
    }
}
