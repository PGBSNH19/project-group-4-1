using System.Collections.Generic;

namespace API.Dtos
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<UserProductDto> UserProducts { get; set; }
        public ICollection<SellerPageProductDto> SellerPageProducts { get; set; }
    }
}
