using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public record WeexFuturesExchangeInfo
    {
        /// <summary>
        /// ["<c>assets</c>"] Assets
        /// </summary>
        [JsonPropertyName("assets")]
        public WeexFuturesExchangeInfoAsset[] Assets { get; set; } = [];
        /// <summary>
        /// ["<c>symbols</c>"] Symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public WeexFuturesSymbol[] Symbols { get; set; } = [];
    }

    /// <summary>
    /// Asset info
    /// </summary>
    public record WeexFuturesExchangeInfoAsset
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginAvailable</c>"] Margin available
        /// </summary>
        [JsonPropertyName("marginAvailable")]
        public bool MarginAvailable { get; set; }
    }

    /// <summary>
    /// Symbol info
    /// </summary>
    public record WeexFuturesSymbol
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseAsset</c>"] Base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteAsset</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginAsset</c>"] Margin asset
        /// </summary>
        [JsonPropertyName("marginAsset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>pricePrecision</c>"] Price precision
        /// </summary>
        [JsonPropertyName("pricePrecision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// ["<c>quantityPrecision</c>"] Quantity precision
        /// </summary>
        [JsonPropertyName("quantityPrecision")]
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// ["<c>baseAssetPrecision</c>"] Base asset precision
        /// </summary>
        [JsonPropertyName("baseAssetPrecision")]
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// ["<c>quotePrecision</c>"] Quote precision
        /// </summary>
        [JsonPropertyName("quotePrecision")]
        public int QuotePrecision { get; set; }
        /// <summary>
        /// ["<c>contractVal</c>"] Size of 1 contract
        /// </summary>
        [JsonPropertyName("contractVal")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// ["<c>delivery</c>"] Delivery schedule
        /// </summary>
        [JsonPropertyName("delivery")]
        public string[] Delivery { get; set; } = [];
        /// <summary>
        /// ["<c>forwardContractFlag</c>"] Forward contract flag
        /// </summary>
        [JsonPropertyName("forwardContractFlag")]
        public bool ForwardContractFlag { get; set; }
        /// <summary>
        /// ["<c>minLeverage</c>"] Min leverage
        /// </summary>
        [JsonPropertyName("minLeverage")]
        public int MinLeverage { get; set; }
        /// <summary>
        /// ["<c>maxLeverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public int MaxLeverage { get; set; }
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
        /// ["<c>makerFeeRate</c>"] Maker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>takerFeeRate</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>minOrderSize</c>"] Min order quantity
        /// </summary>
        [JsonPropertyName("minOrderSize")]
        public decimal MinOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>maxOrderSize</c>"] Max order quantity
        /// </summary>
        [JsonPropertyName("maxOrderSize")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>maxPositionSize</c>"] Max position quantity
        /// </summary>
        [JsonPropertyName("maxPositionSize")]
        public decimal MaxPositionQuantity { get; set; }
        /// <summary>
        /// ["<c>marketOpenLimitSize</c>"] Market open limit quantity
        /// </summary>
        [JsonPropertyName("marketOpenLimitSize")]
        public decimal MarketOpenLimitQuantity { get; set; }
    }


}
