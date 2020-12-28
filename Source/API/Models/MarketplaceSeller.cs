using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class MarketplaceSeller
    {
        [ForeignKey("MarketplaceID")]
        public int MarketplaceID { get; set; }
        public Marketplace Marketplace { get; set; }

        [ForeignKey("SellerID")]
        public int SellerID { get; set; }
        public User Seller { get; set; }
    }
}
