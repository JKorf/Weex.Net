using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Weex.Net.Interfaces;
using Weex.Net.Interfaces.Clients;
using Weex.Net.Objects.Options;

namespace Weex.Net.SymbolOrderBooks
{
    /// <summary>
    /// Weex order book factory
    /// </summary>
    public class WeexOrderBookFactory : IWeexOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public WeexOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;            
            
            Futures = new OrderBookFactory<WeexOrderBookOptions>(CreateFutures, Create);
            Spot = new OrderBookFactory<WeexOrderBookOptions>(CreateSpot, Create);
        }

        
         /// <inheritdoc />
        public IOrderBookFactory<WeexOrderBookOptions> Futures { get; }

         /// <inheritdoc />
        public IOrderBookFactory<WeexOrderBookOptions> Spot { get; }


        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<WeexOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(WeexExchange.FormatSymbol);
            if (symbol.TradingMode == TradingMode.Spot)
                return CreateSpot(symbolName, options);

            return CreateFutures(symbolName, options);
        }

        
         /// <inheritdoc />
        public ISymbolOrderBook CreateFutures(string symbol, Action<WeexOrderBookOptions>? options = null)
            => new WeexFuturesSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IWeexRestClient>(),
                                                          _serviceProvider.GetRequiredService<IWeexSocketClient>());

         /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<WeexOrderBookOptions>? options = null)
            => new WeexSpotSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IWeexRestClient>(),
                                                          _serviceProvider.GetRequiredService<IWeexSocketClient>());


    }
}
