using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using System;

namespace Weex.Net.Objects.Options
{
    /// <summary>
    /// Weex options
    /// </summary>
    public class WeexOptions : LibraryOptions<WeexRestOptions, WeexSocketOptions, WeexCredentials, WeexEnvironment>
    {
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public SharedApiOptions SharedApi { get; set; } = new();
        /// <summary>
        /// Create WeexOptions instance using the provided configuration action
        /// </summary>
        public static WeexOptions Create(Action<WeexOptions>? configure = null)
        {
            var options = CreateUnconfigured();
            configure?.Invoke(options);
            return Normalize(options);
        }

        /// <summary>
        /// Create WeexOptions using the provided IConfiguration
        /// </summary>
        public static WeexOptions CreateFromConfiguration(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var options = CreateUnconfigured();
            try
            {
                configuration.Bind(options);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Invalid Weex configuration provided", ex);
            }

            if (options.Environment != null)
                options.Environment = WeexEnvironment.GetEnvironmentByName(options.Environment.Name) ?? options.Environment;
            if (options.Rest?.Environment != null)
                options.Rest.Environment = WeexEnvironment.GetEnvironmentByName(options.Rest.Environment.Name) ?? options.Rest.Environment;
            if (options.Socket?.Environment != null)
                options.Socket.Environment = WeexEnvironment.GetEnvironmentByName(options.Socket.Environment.Name) ?? options.Socket.Environment;

            return Normalize(options);
        }

        private static WeexOptions CreateUnconfigured()
        {
            var options = new WeexOptions();
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            return options;
        }

        private static WeexOptions Normalize(WeexOptions options)
        {
            if (options.Rest == null)
                throw new ArgumentException("REST options cannot be null", nameof(options));
            if (options.Socket == null)
                throw new ArgumentException("Socket options cannot be null", nameof(options));

            options.Rest.Environment ??= options.Environment ?? WeexEnvironment.Live;
            options.Rest.ApiCredentials ??= options.ApiCredentials;
            options.Socket.Environment ??= options.Environment ?? WeexEnvironment.Live;
            options.Socket.ApiCredentials ??= options.ApiCredentials;
            return options;
        }
    }
}
