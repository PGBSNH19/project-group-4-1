using Newtonsoft.Json;

namespace BlazorApp_Frontend.Data
{
    public class MarketplaceSeller
    {
        [JsonProperty("marketplaceid")]
        public int MarketplaceID { get; set; }

        [JsonProperty("marketplace")]
        public Marketplace Marketplace { get; set; }

        [JsonProperty("sellerid")]
        public int SellerID { get; set; }

        [JsonProperty("seller")]
        public User Seller { get; set; }
    }
}
