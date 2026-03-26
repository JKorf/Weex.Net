using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Weex.Net.Enums;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public record WeexExchangeInfo
    {
        /// <summary>
        /// ["<c>timezone</c>"] Timezone
        /// </summary>
        [JsonPropertyName("timezone")]
        public string Timezone { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>serverTime</c>"] Server time
        /// </summary>
        [JsonPropertyName("serverTime")]
        public DateTime ServerTime { get; set; }
        /// <summary>
        /// ["<c>symbols</c>"] Symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public WeexSymbol[] Symbols { get; set; } = [];
    }

    /// <summary>
    /// Symbol info
    /// </summary>
    public record WeexSymbol
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// ["<c>baseAsset</c>"] Base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseAssetPrecision</c>"] Base asset precision
        /// </summary>
        [JsonPropertyName("baseAssetPrecision")]
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// ["<c>quoteAsset</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteAssetPrecision</c>"] Quote asset precision
        /// </summary>
        [JsonPropertyName("quoteAssetPrecision")]
        public int QuoteAssetPrecision { get; set; }
        /// <summary>
        /// ["<c>tickSize</c>"] Price tick size
        /// </summary>
        [JsonPropertyName("tickSize")]
        public decimal TickSize { get; set; }
        /// <summary>
        /// ["<c>stepSize</c>"] Quantity tick size
        /// </summary>
        [JsonPropertyName("stepSize")]
        public decimal StepSize { get; set; }
        /// <summary>
        /// ["<c>minTradeAmount</c>"] Min trade quantity
        /// </summary>
        [JsonPropertyName("minTradeAmount")]
        public decimal MinTradeQuantity { get; set; }
        /// <summary>
        /// ["<c>maxTradeAmount</c>"] Max trade quantity
        /// </summary>
        [JsonPropertyName("maxTradeAmount")]
        public decimal MaxTradeQuantity { get; set; }
        /// <summary>
        /// ["<c>takerFeeRate</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>makerFeeRate</c>"] Maker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>buyLimitPriceRatio</c>"] Buy limit price ratio
        /// </summary>
        [JsonPropertyName("buyLimitPriceRatio")]
        public decimal BuyLimitPriceRatio { get; set; }
        /// <summary>
        /// ["<c>sellLimitPriceRatio</c>"] Sell limit price ratio
        /// </summary>
        [JsonPropertyName("sellLimitPriceRatio")]
        public decimal SellLimitPriceRatio { get; set; }
        /// <summary>
        /// ["<c>marketBuyLimitSize</c>"] Market buy limit quantity
        /// </summary>
        [JsonPropertyName("marketBuyLimitSize")]
        public decimal MarketBuyLimitQuantity { get; set; }
        /// <summary>
        /// ["<c>marketSellLimitSize</c>"] Market sell limit quantity
        /// </summary>
        [JsonPropertyName("marketSellLimitSize")]
        public decimal MarketSellLimitQuantity { get; set; }
        /// <summary>
        /// ["<c>marketFallbackPriceRatio</c>"] Market fallback price ratio
        /// </summary>
        [JsonPropertyName("marketFallbackPriceRatio")]
        public decimal MarketFallbackPriceRatio { get; set; }
        /// <summary>
        /// ["<c>enableTrade</c>"] Trading enabled
        /// </summary>
        [JsonPropertyName("enableTrade")]
        public bool EnableTrade { get; set; }
        /// <summary>
        /// ["<c>enableDisplay</c>"] Enable display
        /// </summary>
        [JsonPropertyName("enableDisplay")]
        public bool EnableDisplay { get; set; }
        /// <summary>
        /// ["<c>displayDigitMerge</c>"] Depth merge configuration.
        /// </summary>
        [JsonPropertyName("displayDigitMerge")]
        public string DisplayDigitMerge { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>displayNew</c>"] New tag
        /// </summary>
        [JsonPropertyName("displayNew")]
        public bool DisplayNew { get; set; }
        /// <summary>
        /// ["<c>displayHot</c>"] Hot tag
        /// </summary>
        [JsonPropertyName("displayHot")]
        public bool DisplayHot { get; set; }
    }


}
