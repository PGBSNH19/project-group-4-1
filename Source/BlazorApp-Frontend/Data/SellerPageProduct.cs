using Newtonsoft.Json;

namespace BlazorApp_Frontend.Data
{
    public class SellerPageProduct
    {
        [JsonProperty("sellerpageid")]
        public int SellerPageID { get; set; }

        [JsonProperty("sellerpage")]
        public SellerPage sellerPage { get; set; }

        [JsonProperty("productid")]
        public int ProductID { get; set; }

        [JsonProperty("product")]
        public Product product { get; set; }

        [JsonProperty("stock")]
        public int Stock { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }
    }
}
