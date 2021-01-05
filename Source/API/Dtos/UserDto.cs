using API.Models;
using System.Collections.Generic;

namespace API.Dtos
{
    public class UserDto
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }

        public UserType Type { get; set; }
        public ICollection<MarketplaceSeller> MarketplaceSellers { get; set; }
        public ICollection<UserProduct> UserProducts { get; set; }
    }
}
