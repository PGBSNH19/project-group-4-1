using Newtonsoft.Json;

namespace BlazorApp_Frontend.Data
{
    public class ProductPut
    {
        [JsonProperty("productid")]
        public int ProductID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}
