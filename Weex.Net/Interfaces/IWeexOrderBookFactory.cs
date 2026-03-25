using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using System;
using Weex.Net.Objects.Options;

namespace Weex.Net.Interfaces
{
    /// <summary>
    /// Weex local order book factory
    /// </summary>
    public interface IWeexOrderBookFactory
    {
        
        /// <summary>
        /// Futures order book factory methods
        /// </summary>
        IOrderBookFactory<WeexOrderBookOptions> Futures { get; }

        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        IOrderBookFactory<WeexOrderBookOptions> Spot { get; }


        /// <summary>
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, Action<WeexOrderBookOptions>? options = null);

        
        /// <summary>
        /// Create a new Futures local order book instance
        /// </summary>
        ISymbolOrderBook CreateFutures(string symbol, Action<WeexOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new Spot local order book instance
        /// </summary>
        ISymbolOrderBook CreateSpot(string symbol, Action<WeexOrderBookOptions>? options = null);

    }
}