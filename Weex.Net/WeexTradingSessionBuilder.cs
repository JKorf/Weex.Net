using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.TradeSessions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Weex.Net.Interfaces;
using Weex.Net.Interfaces.Clients;

namespace Weex.Net;

/// <inheritdoc />
public class WeexTradingSessionBuilder<TStrategy>
    : TradeSessionBuilder<TStrategy, WeexTradingSessionBuilder<TStrategy>, IWeexTradingUser>
    where TStrategy : WeexTradingStrategy
{
    private readonly IWeexOrderBookFactory _orderBookFactory;
    internal IWeexTrackerFactory Trackers { get; }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="clientProvider"></param>
    /// <param name="orderBookFactory"></param>
    /// <param name="trackerFactory"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public WeexTradingSessionBuilder(
        IWeexUserClientProvider clientProvider,
        IWeexOrderBookFactory orderBookFactory,
        IWeexTrackerFactory trackerFactory,
        ILogger<WeexTradingSessionBuilder<TStrategy>> logger)
        : base(clientProvider, logger, context => new WeexSessionUser(context, clientProvider))
    {
        _orderBookFactory = orderBookFactory ?? throw new ArgumentNullException(nameof(orderBookFactory));
        Trackers = trackerFactory ?? throw new ArgumentNullException(nameof(trackerFactory));
    }

    /// <summary>
    /// Add symbol tracking
    /// </summary>
    /// <param name="symbol">The symbol to track</param>
    /// <param name="configure">Symbol tracking configuration</param>
    /// <param name="environment">Trading environment</param>
    public WeexTradingSessionBuilder<TStrategy> AddSymbolTracking(
        SharedSymbol symbol,
        Action<WeexTradingSessionSymbolBuilder<TStrategy>> configure,
        WeexEnvironment? environment = null)
    {
        RegisterSymbol(
            new WeexTradingSessionSymbolBuilder<TStrategy>(symbol, _orderBookFactory, environment),
            configure);
        return this;
    }

    /// <summary>
    /// Add the same symbol tracking configuration for each symbol
    /// </summary>
    /// <param name="symbols">The symbols to track</param>
    /// <param name="configure">Symbol tracking configuration, called once for each symbol</param>
    /// <param name="environment">Trading environment</param>
    public WeexTradingSessionBuilder<TStrategy> AddSymbolTracking(
        IEnumerable<SharedSymbol> symbols,
        Action<WeexTradingSessionSymbolBuilder<TStrategy>> configure,
        WeexEnvironment? environment = null)
    {
        RegisterSymbols(
            symbols,
            symbol => new WeexTradingSessionSymbolBuilder<TStrategy>(symbol, _orderBookFactory, environment),
            configure);
        return this;
    }

    /// <summary>
    /// Add a session user
    /// </summary>
    /// <param name="userIdentifier">User identifier, should be unique</param>
    /// <param name="configure">User configuration</param>
    /// <param name="environment">Trading environment</param>
    public WeexTradingSessionBuilder<TStrategy> AddUser(
        string userIdentifier,
        Action<TradeSessionUserBuilder<TStrategy, WeexEnvironment>> configure,
        WeexEnvironment? environment = null)
    {
        RegisterUser(userIdentifier, configure, Trackers, environment);
        return this;
    }
}
