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
    /// User trade info
    /// </summary>
    public record WeexFuturesUserTrade
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>buyer</c>"] Is buyer
        /// </summary>
        [JsonPropertyName("buyer")]
        public bool IsBuyer { get; set; }
        /// <summary>
        /// ["<c>commission</c>"] Fee
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>commissionAsset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("commissionAsset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>maker</c>"] Is maker
        /// </summary>
        [JsonPropertyName("maker")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>qty</c>"] Quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>quoteQty</c>"] Quote quantity
        /// </summary>
        [JsonPropertyName("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>realizedPnl</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("realizedPnl")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Trade time
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }


}
