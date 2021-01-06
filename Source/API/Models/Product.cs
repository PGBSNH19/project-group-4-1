using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace API.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public byte[] PictureBytes { get; set; }
        public ICollection<UserProduct> UserProducts { get; set; }
        public ICollection<SellerPageProduct> SellerPageProducts { get; set; }
    }
}
