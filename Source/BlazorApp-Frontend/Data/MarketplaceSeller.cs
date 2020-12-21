namespace BlazorApp_Frontend.Data
{
    public class MarketplaceSeller
    {
        public int MarketplaceID { get; set; }
        public Marketplace Marketplace { get; set; }

        public int SellerID { get; set; }
        public User Seller { get; set; }
    }
}
