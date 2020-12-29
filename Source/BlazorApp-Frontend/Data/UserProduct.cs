using Newtonsoft.Json;

namespace BlazorApp_Frontend.Data
{
    public class UserProduct
    {
        [JsonProperty("userid")]
        public int UserID { get; set; }

        [JsonProperty("user")]
        public User user { get; set; }

        [JsonProperty("productid")]
        public int ProductID { get; set; }

        [JsonProperty("product")]
        public Product product { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}
