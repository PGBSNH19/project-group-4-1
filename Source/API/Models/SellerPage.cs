using System.Collections.Generic;

namespace API.Models
{
    public class SellerPage
    {
        public int ID { get; set; }

        public int Name { get; set; }

        public User Seller { get; set; }

        public List<Products> products { get; set; }
    }
}
