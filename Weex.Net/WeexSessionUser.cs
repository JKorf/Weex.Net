using CryptoExchange.Net.Trackers.Sessions;
using System;
using Weex.Net.Interfaces;
using Weex.Net.Interfaces.Clients;

namespace Weex.Net;

/// <summary>
/// Weex trading session user
/// </summary>
internal sealed class WeexSessionUser : TradingSessionUser, IWeexTradingUser
{
    public IWeexSpotTradingUser Spot { get; }
    public IWeexFuturesTradingUser Futures { get; }

    internal WeexSessionUser(TradingSessionUserContext context, IWeexUserClientProvider provider)
        : base(context)
    {
        if (Environment != null && Environment is not WeexEnvironment)
            throw new ArgumentException("A WeexEnvironment is required.", nameof(context));

        Spot = new WeexSpotTradingUser(UserIdentifier, (WeexEnvironment?)Environment, provider);
        Futures = new WeexFuturesTradingUser(UserIdentifier, (WeexEnvironment?)Environment, provider);
    }
}
