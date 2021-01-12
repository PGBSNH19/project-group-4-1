using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BlazorApp_Frontend.Data
{
    public class User
    {
        [JsonProperty("userid")]
        public int UserID { get; set; }

        [JsonProperty("username")]
        [Required(ErrorMessage = "Användarnamn måste anges")]
        [MaxLength(125, ErrorMessage = "Användarnamn är inte giltigt")]
        public string Username { get; set; }

        [JsonProperty("email")]
        [Required(ErrorMessage = "E-post måste anges")]
        [EmailAddress(ErrorMessage = "Ange en giltig e-post address")]
        public string Email { get; set; }

        [JsonProperty("password")]
        [Required(ErrorMessage = "Lösenord måste anges")]
        [MinLength(10, ErrorMessage = "Lösenordet måste vara minst 10 karaktärer långt")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lösenord måste anges")]
        [MinLength(10, ErrorMessage = "Lösenordet måste vara minst 10 karaktärer långt")]
        [Compare("Password",
            ErrorMessage = "Lösenorden måste matcha")]
        public string ConfirmPassword { get; set; }

        [JsonProperty("salt")]
        public byte[] Salt { get; set; }

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