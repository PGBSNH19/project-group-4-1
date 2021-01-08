using API.Models;

namespace API.Dtos
{
    public class MarketplaceSellerDto
    {
        public int MarketplaceID { get; set; }
        public MarketplaceDto Marketplace { get; set; }

        public int SellerID { get; set; }
        public User Seller { get; set; }
    }
}
