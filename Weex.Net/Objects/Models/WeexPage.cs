using System.Text.Json.Serialization;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Data page
    /// </summary>
    public record WeexPage<T>
    {
        /// <summary>
        /// Total number of results
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// Results per page
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        /// <summary>
        /// Total pages
        /// </summary>
        [JsonPropertyName("pages")]
        public int? Pages { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }
        /// <summary>
        /// Has next page
        /// </summary>
        [JsonPropertyName("hasNextPage")]
        public bool HasNextPage { get; set; }
        /// <summary>
        /// Data items
        /// </summary>
        [JsonPropertyName("items")]
        public T[] Items { get; set; } = [];
    }
}
