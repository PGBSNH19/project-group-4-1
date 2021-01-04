using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class SellerPage
    {
        [Key]
        public int SellerPageID { get; set; }
        public string Name { get; set; }
        public int SellerUserID { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public string Description { get; set; }
        public User Seller { get; set; }
        public ICollection<SellerPageProduct> SellerPageProducts { get; set; }
    }
}
