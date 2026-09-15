using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;
using Weex.Net;
using Weex.Net.Clients;
using Weex.Net.Interfaces;
using Weex.Net.Interfaces.Clients;
using Weex.Net.Objects.Options;
using Weex.Net.SymbolOrderBooks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add services such as the IWeexRestClient and IWeexSocketClient. Configures the services based on the provided configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddWeex(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = WeexOptions.CreateFromConfiguration(configuration);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddWeexCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the IWeexRestClient and IWeexSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the Weex services</param>
        /// <returns></returns>
        public static IServiceCollection AddWeex(
            this IServiceCollection services,
            Action<WeexOptions>? optionsDelegate = null)
        {
            var options = WeexOptions.Create(optionsDelegate);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddWeexCore(services, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddWeexCore(
            this IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IWeexRestClient, WeexRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<WeexRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new WeexRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<WeexRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var options = serviceProvider.GetRequiredService<IOptions<WeexRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            services.Add(new ServiceDescriptor(typeof(IWeexSocketClient), x => { return new WeexSocketClient(x.GetRequiredService<IOptions<WeexSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<IWeexOrderBookFactory, WeexOrderBookFactory>();
            services.AddTransient<ITrackerFactory, WeexTrackerFactory>();
            services.AddTransient<IWeexTrackerFactory, WeexTrackerFactory>();
            services.AddSingleton<IWeexUserClientProvider, WeexUserClientProvider>(x =>
                new WeexUserClientProvider(
                    x.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(IWeexRestClient).Name),
                    x.GetRequiredService<ILoggerFactory>(),
                    x.GetRequiredService<IOptions<WeexRestOptions>>(),
                    x.GetRequiredService<IOptions<WeexSocketOptions>>()));

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IWeexRestClient>().FuturesApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IWeexSocketClient>().FuturesApi.SharedClient);
            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IWeexRestClient>().SpotApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IWeexSocketClient>().SpotApi.SharedClient);

            services.RegisterSharedApiClient<
                IWeexSharedApiClient,
                WeexSharedApiClient>(sharedApis => sharedApis
                    .Add(client => client.SpotRest)
                    .Add(client => client.SpotSocket)
                    .Add(client => client.FuturesRest)
                    .Add(client => client.FuturesSocket)
                    );

            return services;
        }
    }
}
