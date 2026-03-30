using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.Logging;
using Weex.Net.Clients;
using Weex.Net.Interfaces.Clients;
using Weex.Net.Objects.Models;
using Weex.Net.Objects.Options;

namespace Weex.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class WeexFuturesSymbolOrderBook : SymbolOrderBook
    {
        private readonly bool _clientOwner;
        private readonly IWeexSocketClient _socketClient;
        private readonly TimeSpan _initialDataTimeout;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public WeexFuturesSymbolOrderBook(string symbol, Action<WeexOrderBookOptions>? optionsDelegate = null)
            : this(symbol, optionsDelegate, null, null, null)
        {
            _clientOwner = true;
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="restClient">Rest client instance</param>
        /// <param name="socketClient">Socket client instance</param>
        public WeexFuturesSymbolOrderBook(
            string symbol,
            Action<WeexOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            IWeexRestClient? restClient,
            IWeexSocketClient? socketClient) : base(logger, "Weex", "Futures", symbol)
        {
            var options = WeexOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
            _sequencesAreConsecutive = true;

            Levels = options?.Limit ?? 15;
            _initialDataTimeout = options?.InitialDataTimeout ?? TimeSpan.FromSeconds(30);
            _clientOwner = socketClient == null;
            _socketClient = socketClient ?? new WeexSocketClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            var result = await _socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync(Symbol, Levels!.Value, ProcessUpdate).ConfigureAwait(false);
            if (!result)
                return result;

            if (ct.IsCancellationRequested)
            {
                await result.Data.CloseAsync().ConfigureAwait(false);
                return result.AsError<UpdateSubscription>(new CancellationRequestedError());
            }

            Status = OrderBookStatus.Syncing;

            var setResult = await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
            if (!setResult)
                await result.Data.CloseAsync().ConfigureAwait(false);

            return setResult ? result : new CallResult<UpdateSubscription>(setResult.Error!);
        }

        private void ProcessUpdate(DataEvent<WeexOrderBookUpdate> @event)
        {
            if (@event.UpdateType == SocketUpdateType.Snapshot)
                SetSnapshot(@event.Data.LastUpdateId, @event.Data.Bids, @event.Data.Asks, @event.DataTime, @event.DataTimeLocal);
            else
                UpdateOrderBook(@event.Data.FirstUpdateId + 1, @event.Data.LastUpdateId, @event.Data.Bids, @event.Data.Asks, @event.DataTime, @event.DataTimeLocal);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            return await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (_clientOwner)            
                _socketClient?.Dispose();

            base.Dispose(disposing);
        }
    }
}
