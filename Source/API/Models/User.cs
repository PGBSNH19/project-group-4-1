using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        public ICollection<MarketplaceSeller> MarketplaceSellers { get; set; }
        public ICollection<UserProduct> UserProducts { get; set; }
        public byte[] Salt { get; set; }
    }
    public enum UserType
    {
        Buyer,
        Seller,
        Admin
    }
}
