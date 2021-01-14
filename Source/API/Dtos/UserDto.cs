using API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class UserDto
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public UserType Type { get; set; }
        public ICollection<MarketplaceSellerDto> MarketplaceSellers { get; set; }
        public ICollection<UserProductDto> UserProducts { get; set; }
    }
}
