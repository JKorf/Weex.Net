using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.RateLimiting;
using System;
using CryptoExchange.Net.SharedApis;
using Weex.Net.Converters;
using System.Text.Json;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Filters;

namespace Weex.Net
{
    /// <summary>
    /// Weex exchange information and configuration
    /// </summary>
    public static class WeexExchange
    {
        /// <summary>
        /// Platform metadata
        /// </summary>
        public static PlatformInfo Metadata { get; } = new PlatformInfo(
                "Weex",
                "Weex",
                "https://raw.githubusercontent.com/JKorf/Weex.Net/main/Weex.Net/Icon/icon.png",
                "https://www.weex.com/",
                ["https://www.weex.com/api-doc/spot/log/changelog"],
                PlatformType.CryptoCurrencyExchange,
                CentralizationType.Centralized
                );

        internal static JsonSerializerOptions _serializerContext = SerializerOptions.WithConverters(JsonSerializerContextCache.GetOrCreate<WeexSourceGenerationContext>());

        /// <summary>
        /// Aliases for Weex assets
        /// </summary>
        public static AssetAliasConfiguration AssetAliases { get; } = new AssetAliasConfiguration
        {
            Aliases = [
                new AssetAlias("USDT", SharedSymbol.UsdOrStable.ToUpperInvariant(), AliasType.OnlyToExchange)
            ]
        };

        /// <summary>
        /// Format a base and quote asset to an Weex recognized symbol 
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            baseAsset = AssetAliases.CommonToExchangeName(baseAsset.ToUpperInvariant());
            quoteAsset = AssetAliases.CommonToExchangeName(quoteAsset.ToUpperInvariant());

            return baseAsset + quoteAsset;
        }

        /// <summary>
        /// Rate limiter configuration for the Weex API
        /// </summary>
        public static WeexRateLimiters RateLimiter { get; } = new WeexRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the Weex API
    /// </summary>
    public class WeexRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;
        /// <summary>
        /// Event when the rate limit is updated. Note that it's only updated when a request is send, so there are no specific updates when the current usage is decaying.
        /// </summary>
        public event Action<RateLimitUpdateEvent> RateLimitUpdated;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal WeexRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            WeexRestIp = new RateLimitGate("Weex IP")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, new LimitItemTypeFilter(RateLimitItemType.Request), 500, TimeSpan.FromSeconds(10), RateLimitWindowType.Sliding)); // 500 weight per 10 seconds 
            WeexRestUid = new RateLimitGate("Weex UID")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, new LimitItemTypeFilter(RateLimitItemType.Request), 500, TimeSpan.FromSeconds(10), RateLimitWindowType.Sliding)); // 500 weight per 10 seconds 
            WeexSocket = new RateLimitGate("Weex Socket")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new LimitItemTypeFilter(RateLimitItemType.Connection), 300, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding)) // 300 connections per 5 minutes 
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new LimitItemTypeFilter(RateLimitItemType.Request), 240, TimeSpan.FromMinutes(60), RateLimitWindowType.Sliding)); // 240 operations per hour per connection 

            WeexRestIp.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            WeexRestIp.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
            WeexRestUid.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            WeexRestUid.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
            WeexSocket.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            WeexSocket.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
        }


        internal IRateLimitGate WeexSocket { get; private set; }
        internal IRateLimitGate WeexRestIp { get; private set; }
        internal IRateLimitGate WeexRestUid { get; private set; }

    }
}
