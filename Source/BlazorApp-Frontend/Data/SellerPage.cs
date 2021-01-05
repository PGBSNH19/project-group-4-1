using System.Collections.Generic;

namespace BlazorApp_Frontend.Data
{
    public class SellerPage
    {
        public int SellerPageID { get; set; }

        public string Name { get; set; }

        public int SellerUserID { get; set; }

        public User Seller { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<SellerPageProduct> SellerPageProducts { get; set; }
    }
}
