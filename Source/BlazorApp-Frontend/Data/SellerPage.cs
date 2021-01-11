using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlazorApp_Frontend.Data
{
    public class SellerPage
    {
        [JsonProperty("sellerpageid")]
        public int SellerPageID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("selleruserid")]
        public int SellerUserID { get; set; }

        [JsonProperty("seller")]
        public User Seller { get; set; }

        [JsonProperty("sellerpageproducts")]
        public ICollection<SellerPageProduct> SellerPageProducts { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}