using API.Models;

namespace API.Dtos
{
    public class SellerPageProductDto
    {
        public int SellerPageID { get; set; }
        public SellerPage sellerPage { get; set; }
        public int ProductID { get; set; }
        public Product product { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
    }
}
