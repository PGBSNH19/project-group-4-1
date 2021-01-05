using Newtonsoft.Json;
using System.Collections.Generic;


namespace BlazorApp_Frontend.Data
{
    public class User
    {
        [JsonProperty("userid")]
        public int UserID { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("type")]
        public UserType Type { get; set; }

        [JsonProperty("marketplacesellers")]
        public ICollection<MarketplaceSeller> MarketplaceSellers { get; set; }

        [JsonProperty("userproducts")]
        public ICollection<UserProduct> UserProducts { get; set; }
    }

    public enum UserType
    {
        Buyer,
        Seller,
        Admin
    }
}
