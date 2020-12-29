using System.Collections.Generic;
using API.Models;
using Microsoft.AspNetCore.Http;

namespace API.Dtos
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public IFormFile Picture { get; set; }
        public ICollection<UserProduct> UserProducts { get; set; }
        public ICollection<SellerPageProduct> SellerPageProducts { get; set; }
    }
}
