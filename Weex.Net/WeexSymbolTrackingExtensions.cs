using CryptoExchange.Net.Sessions;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Sessions.Features;
using CryptoExchange.Net.Trackers.Sessions.Interfaces;
using System;
using Weex.Net.Objects.Options;

namespace Weex.Net
{
    public static class WeexSymbolTrackingExtensions
    {
        public static WeexSymbolTrackingBuilder<TSession> AddOrderBookTracking<TSession>(
            this WeexSymbolTrackingBuilder<TSession> builder,
            Action<WeexOrderBookOptions>? options = null)
            where TSession : Session, IOrderBookUpdateHandler
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            var symbol = builder.Symbol;
            var environment = builder.Environment;
            if (symbol.TradingMode != TradingMode.Spot && symbol.TradingMode != TradingMode.PerpetualLinear)
                throw new NotSupportedException($"Weex order books do not support {symbol.TradingMode}.");

            var factory = builder.OrderBooks;
            return builder.RegisterOrderBook(session =>
                new OrderBookFeature(factory.Create(symbol, options, (WeexEnvironment?)environment), symbol, session));
        }
    }
}
