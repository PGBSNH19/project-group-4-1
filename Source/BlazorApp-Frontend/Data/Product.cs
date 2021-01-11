namespace BlazorApp_Frontend.Data
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }

        [JsonProperty("Image")]
        public string Image { get; set; }

        [JsonProperty("userproducts")]
        public ICollection<UserProduct> UserProducts { get; set; }

        [JsonProperty("sellerpageproducts")]
        public ICollection<SellerPageProduct> SellerPageProducts { get; set; }
    }
}
