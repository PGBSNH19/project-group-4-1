﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlazorApp_Frontend.Data
{
    public class Product
    {
        [JsonProperty("productid")]
        public int ProductID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("stock")]
        public int Stock { get; set; }

        [JsonProperty("userproduct")]
        public ICollection<UserProduct> UserProducts { get; set; }

        [JsonProperty("sellerpageproducts")]
        public ICollection<SellerPageProduct> SellerPageProducts { get; set; }
    }
}
