using CryptoExchange.Net.TradeSessions;
using System;
using Weex.Net.Interfaces;
using Weex.Net.Interfaces.Clients;

namespace Weex.Net;

/// <summary>
/// Weex trading session user
/// </summary>
internal sealed class WeexSessionUser : TradeSessionUser, IWeexTradingUser
{
    public IWeexSpotTradingUser Spot { get; }
    public IWeexFuturesTradingUser Futures { get; }

    internal WeexSessionUser(TradeSessionUserContext context, IWeexUserClientProvider provider)
        : base(context)
    {
        if (Environment != null && Environment is not WeexEnvironment)
            throw new ArgumentException("A WeexEnvironment is required.", nameof(context));

        Spot = new WeexSpotTradingUser(UserIdentifier, (WeexEnvironment?)Environment, provider);
        Futures = new WeexFuturesTradingUser(UserIdentifier, (WeexEnvironment?)Environment, provider);
    }
}
