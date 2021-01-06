using API.Models;

namespace API.Dtos
{
    public class UserProductDto
    {
        public int UserID { get; set; }
        public User user { get; set; }
        public int ProductID { get; set; }
        public Product product { get; set; }
        public int Amount { get; set; }

    }
}
