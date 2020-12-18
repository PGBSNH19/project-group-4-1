using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class SellerPageProduct
    {
        [ForeignKey("SellerPageID")]
        public int SellerPageID { get; set; }
        public SellerPage sellerPage { get; set; }

        [ForeignKey("ProductsID")]
        public int ProductID { get; set; }
        public Product product { get; set; }

        public int Stock { get; set; }
        public int Price { get; set; }
    }
}
